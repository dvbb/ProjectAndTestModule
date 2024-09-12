using RabbitMQ.Client;

namespace CodeMan.Rabbitmq.Common
{
    public class RabbitUtils
    {
        public static ConnectionFactory GetConnection()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "127.0.0.1";
            factory.Port = 5672;//5672是RabbitMQ默认的端口号
            factory.UserName = "guest";
            factory.Password = "guest";
            //factory.VirtualHost = "my_vhost";
            return factory;
        }
    }
}