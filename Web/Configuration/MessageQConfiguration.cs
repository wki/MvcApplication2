using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Web.Configuration
{
    public sealed class MessageQConfiguration : ConfigurationSection
    {
        private static MessageQConfiguration instance = null;

        public static MessageQConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (MessageQConfiguration)WebConfigurationManager.GetSection("messageQ");
                }

                return instance;
            }
        }

        // <messageQ host="localhost" port="5672" user="guest" password="guest" vhost="/" />

        [ConfigurationProperty("host", DefaultValue="localhost")]
        public string Host
        {
            get { return (string)base["host"]; }
            set { base["host"] = value; } 
        }

        [ConfigurationProperty("port", DefaultValue="5672")]
        public int Port
        {
            get { return (int)base["port"]; }
            set { base["port"] = value; }
        }

        [ConfigurationProperty("vhost", DefaultValue = "/")]
        public string VirtualHost
        {
            get { return (string)base["vhost"]; }
            set { base["vhost"] = value; }
        }

        [ConfigurationProperty("user", DefaultValue = "guest")]
        public string User
        {
            get { return (string)base["user"]; }
            set { base["user"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue="guest")]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

    }
}