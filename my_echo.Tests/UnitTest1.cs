using NUnit.Framework;
using System;
using System.IO;
using my_echo;

namespace my_echo.Tests
{
    [TestFixture]
    public class EchoTests
    {
        [Test]
        public void Run_WithTextArguments_OutputsToStdout()
        {
            var input = new StringReader("");
            var output = new StringWriter();
            var error = new StringWriter();

            int exitCode = App.Run(new[] { "hello", "world" }, input, output, error);

            Assert.That(exitCode, Is.EqualTo(0));
            Assert.That(output.ToString(), Is.EqualTo($"hello world{Environment.NewLine}"));
            Assert.That(error.ToString(), Is.Empty);
        }

        [Test]
        public void Run_NoArguments_ReadsFromStdin()
        {
            string stdinText = "Це текст зі стандартного вводу\nДругий рядок";
            var input = new StringReader(stdinText);
            var output = new StringWriter();
            var error = new StringWriter();

            int exitCode = App.Run(Array.Empty<string>(), input, output, error);

            Assert.That(exitCode, Is.EqualTo(0));
            Assert.That(output.ToString(), Is.EqualTo(stdinText));
            Assert.That(error.ToString(), Is.Empty);
        }

        [Test]
        public void Run_WithOptionN_OmitsNewline()
        {
            var input = new StringReader("");
            var output = new StringWriter();
            var error = new StringWriter();

            int exitCode = App.Run(new[] { "-n", "hello" }, input, output, error);

            Assert.That(exitCode, Is.EqualTo(0));
            Assert.That(output.ToString(), Is.EqualTo("hello")); 
            Assert.That(error.ToString(), Is.Empty);
        }

        [Test]
        public void Run_UnknownOption_ReturnsExitCode2_WritesToStderr()
        {
            var input = new StringReader("");
            var output = new StringWriter();
            var error = new StringWriter();

            int exitCode = App.Run(new[] { "--unknown-option" }, input, output, error);

            Assert.That(exitCode, Is.EqualTo(2));
            Assert.That(output.ToString(), Is.Empty);
            Assert.That(error.ToString(), Does.Contain("unknown").IgnoreCase);
        }
    }
}