using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQ
{
    class Options
    {
        [Option('m', "message", Required = false,
        HelpText = "Message to send")]
        public string Message { get; set; }

        [Option('r', "route", DefaultValue = "Render", Required = false,
        HelpText = "routing key to use")]
        public string RoutingKey { get; set; }

        [Option('v', "verbose", DefaultValue = false,
        HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    class Command { }
    class RenderCommand : Command
    {
        public string Message { get; set; }
        public int Count { get; set; }
    }

    class ImportCommand : Command
    {
        public string Message { get; set; }
        public int Whatever { get; set; }
    }

    class PingCommand : Command
    {
        public string Message { get; set; }
    }

    class Publish
    {
        public static void Main(string[] args)
        {

            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                PublishMessage(options);
            }

            Environment.Exit(0);
        }

        private static void PublishMessage(Options options)
        {
            var messageQ = new MessageQ();
            Command data;
            if (options.RoutingKey.ToLower() == "ping")
            {
                data = new PingCommand { Message = options.Message };
                messageQ.Broadcast(data);
            }
            else if (options.RoutingKey.ToLower() == "render")
            {
                data = new RenderCommand { Message = options.Message, Count = 42 };
                messageQ.Publish(data);
            }
            else
            {
                data = new ImportCommand { Message = options.Message, Whatever = 33 };
                messageQ.Publish(data);
            }
            
            if (options.Verbose) Console.WriteLine("published.");
        }
    }
}
