﻿using System;
using System.IO;
using System.Reflection;
using CommandLine;
using RobbersLang.IO;

namespace ConsoleApp
{
    internal class Program
    {
        public class Options
        {
            [Value(0, Required = true, HelpText = "Input file. The path to the text file to read from.")]
            public string InputFilePath { get; set; }

            [Option('o', "output", Required = true,
                HelpText =
                    "Output file. The path to the text file to write to. (If the file does not exist, it is created. If the file already exists, its contents are overwritten.)")]
            public string OutputFilePath { get; set; }

            [Option('d', "decode", Required = false,
                HelpText =
                    "If specified, tells the program to decode from 'Rövarspråket'. (Otherwise, the input file is expected to contain plain text to be encoded.)")]
            public bool Decode { get; set; }
        }

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    Console.WriteLine(!options.Decode ? "Encoding from {0} to {1}..." : "Decoding from {0} to {1}...",
                        options.InputFilePath, options.OutputFilePath);

                    using (var reader = OpenReader(options))
                    using (var writer = OpenWriter(options))
                    {
                        var buffer = new Span<char>(new char[512]);
                        int count;

                        while ((count = reader.Read(buffer)) > 0)
                            writer.Write(buffer.Slice(0, count));
                    }

                    Console.WriteLine("Done.");
                })
                .WithNotParsed(errors =>
                {
                    Console.WriteLine($@"Examples:

dotnet {Path.GetFileName(Assembly.GetEntryAssembly().Location)} sample-plain-text.txt --output sample-robbers-language.txt
dotnet {Path.GetFileName(Assembly.GetEntryAssembly().Location)} sample-robbers-language.txt --decode --output back-to-pain-text.txt
");
                });
        }

        private static TextReader OpenReader(Options options)
        {
            return options.Decode
                ? (TextReader) new RobbersLangReader(File.OpenText(options.InputFilePath))
                : File.OpenText(options.InputFilePath);
        }

        private static TextWriter OpenWriter(Options options)
        {
            return options.Decode
                ? File.CreateText(options.OutputFilePath)
                : (TextWriter) new RobbersLangWriter(File.CreateText(options.OutputFilePath));
        }
    }
}