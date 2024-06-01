using CSRedis;
using GameManagement.HttpApi.Redis;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;

namespace GameManagement.HttpApi.Redis
{
    public class RedisTool : IRedisTool
    {
        CSRedisClient csRedis;

        public RedisTool(string redisConfig)
        {
            csRedis = new CSRedisClient(redisConfig);
        }

        /// <summary>
        /// 设置长时间存在的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetLongValue(string key, string value)
        {
            try
            {
                csRedis.Set(key, value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置值，并设置清除时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="outSecond"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value, int outSecond)
        {
            try
            {
                csRedis.Set(key, value, outSecond);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置值，存在则覆盖，并沿用之前的清除时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetValue(string key, string value)
        {
            try
            {
                if (csRedis.Exists(key))
                {
                    long time = csRedis.Ttl(key);
                    csRedis.Set(key, value, Convert.ToInt32(time));
                }
                else
                    csRedis.Set(key, value);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 是否存在key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            try
            {
                return csRedis.Exists(key);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新Key，把自动注销时间设置为原来的key的时间,不存在返回false
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateValue(string key, string value)
        {
            try
            {
                if (csRedis.Exists(key))
                {
                    long time = csRedis.Ttl(key);
                    csRedis.Set(key, value, Convert.ToInt32(time));
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public string? GetValue(string key)
        {
            try
            {
                return csRedis.Get(key);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获得json序列化后的
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? GetValue<T>(string key)
        {
            try
            {
                var data = csRedis.Get(key);
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public T? GetEntity<T>(string key)
        {
            try
            {
                var data = csRedis.Get(key);
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public List<T>? GetLike<T>(string key)
        {
            try
            {
                var dataList = csRedis.Keys(key + "*");
                List<T> list = new List<T>();
                foreach (string item in dataList)
                {
                    var data = GetEntity<T>(item);
                    if (data != null)
                    {
                        list.Add(data);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public void DeleteKey(string key)
        {
            try
            {
                csRedis.Del(key);
            }
            catch (Exception ex)
            {
            }
        }

        public void DeleteLike(string key)
        {
            try
            {
                var dataList = csRedis.Keys(key + "*");

                foreach (string item in dataList)
                {
                    DeleteKey(item);
                }
            }
            catch (Exception ex)
            {
            }
        }


        private bool AcquireLock(string lockKey, string lockValue, int lockTimeoutSeconds)
        {
            // 尝试获取锁
            bool lockAcquired = csRedis.SetNx(lockKey, lockValue);

            // 如果成功获取锁，设置锁的超时时间
            if (lockAcquired)
            {
                csRedis.Expire(lockKey, lockTimeoutSeconds);
            }

            return lockAcquired;
        }

        private void ReleaseLock(string lockKey, string lockValue)
        {
            // 释放锁
            // 使用 Lua 脚本确保只有持有锁的客户端才能释放锁
            string luaScript = @"
            if redis.call('get', KEYS[1]) == ARGV[1] then
                return redis.call('del', KEYS[1])
            else
                return 0
            end";

            csRedis.Eval(luaScript, lockKey, new[] { lockValue });

        }
    }
}
