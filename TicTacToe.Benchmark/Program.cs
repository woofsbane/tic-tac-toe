using BenchmarkDotNet.Running;

BenchmarkRunner.Run(typeof(Program).Assembly);

Console.ReadLine();