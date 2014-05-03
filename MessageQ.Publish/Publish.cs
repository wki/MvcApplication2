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

        [Option('r', "route", DefaultValue = "render", Required = false,
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
            var data = new { foo = options.Message, baz = 42 };
            messageQ.Publish(options.RoutingKey, data);
            if (options.Verbose) Console.WriteLine("published.");
        }
    }
}
