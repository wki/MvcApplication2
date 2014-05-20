using MessageQ;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Web.Configuration
{
    public sealed class MessageQConfiguration 
        : ConfigurationSection, IMessageQConfiguration
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

        [ConfigurationProperty("hostname", DefaultValue="localhost")]
        public string HostName
        {
            get { return (string)base["hostname"]; }
            set { base["hostname"] = value; } 
        }

        [ConfigurationProperty("port", DefaultValue="5672")]
        public int Port
        {
            get { return (int)base["port"]; }
            set { base["port"] = value; }
        }

        [ConfigurationProperty("virtualhost", DefaultValue = "/")]
        public string VirtualHost
        {
            get { return (string)base["virtualhost"]; }
            set { base["virtualhost"] = value; }
        }

        [ConfigurationProperty("username", DefaultValue = "guest")]
        public string UserName
        {
            get { return (string)base["username"]; }
            set { base["username"] = value; }
        }

        [ConfigurationProperty("password", DefaultValue="guest")]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }

    }
}