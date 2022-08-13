using MyAdapterPattern.Models;
using System;

namespace MyAdapterPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student stu = new Student();

            IHelper mysqlHelper = new MysqlHelper();
            mysqlHelper.Add(stu);
            mysqlHelper.Delete(stu);

            IHelper sqlServerHelper = new SqlServerHelper();
            sqlServerHelper.Add(stu);
            sqlServerHelper.Delete(stu);

            /// 假设RedisHelper是一个外部dll的类，则无法使用IHelper接受实例
            /// 此时需要适配器
            //IHelper redisHelper = new RedisHelper();

            /// 编写一个适配器RedisObjectHelper继承IHelper,在适配器中调用外部DLL类RedisHelper,达到使用体验一致
            IHelper redisHelper = new RedisObjectHelper();
            redisHelper.Add(stu);
            redisHelper.Delete(stu);
        }

    }

    internal class RedisObjectHelper : IHelper
    {
        private RedisHelper _redisHelper;

        public RedisObjectHelper()
        {
            _redisHelper = new RedisHelper();
        }

        public void Add<T>(T t)
        {
            _redisHelper.AddRedis<T>();
        }

        public void Delete<T>(T t)
        {
            _redisHelper.DeleteRedis<T>();
        }
    }

    internal class Student
    {
        public int Id { get; set; }
    }
}
