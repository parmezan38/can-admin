using CANAdmin.Shared.Models;
using CANAdmin.Shared.Tools;
using CANAdmin.Shared.Tools.LineParsers;
using System.Collections.Generic;
using Xunit;

namespace CANAdmin.Tests
{
    public class DbcParsingTest
    {
        [Fact]
        public void DbcParserShouldReturnCorrectValues()
        {
            var dbcParser = new DbcParser();

            string location = "../../../DbcExamples/All_msg_types.dbc";
            FileModel file = new FileModel("All_msg_types.dbc", location);

            ParsingArguments parsingArguments = new ParsingArguments()
            {
                Messages = { "MessageId", "Name" },
                Signals = { "Name", "StartBit", "Length" }
            };

            var result = dbcParser.ParseFile(file, parsingArguments);

            int networkNodescount = result.NetworkNodes.Count;
            Assert.True(networkNodescount == 20);

            int totalMessageCount = result.Messages.Count;
            Assert.True(totalMessageCount == 21);

            int totalSignalCount = 0;
            foreach (Message message in result.Messages)
            {
                foreach (Signal signal in message.Signals)
                {
                    Assert.True(signal != null);
                    totalSignalCount++;
                }
            }
            Assert.True(totalSignalCount == 256);

            int lastMessageSignalCount = result.Messages[totalMessageCount - 1].Signals.Count;
            Assert.True(lastMessageSignalCount == 71);

            var firstSignal = result.Messages[0].Signals[0];
            Assert.True(firstSignal.Name == "Sig_BE_UInt_2" && firstSignal.StartBit == 0 && firstSignal.Length == 7);

            var lastSignal = result.Messages[totalMessageCount - 1].Signals[lastMessageSignalCount - 1];
            Assert.True(lastSignal.Name == "cell_voltage_070" && lastSignal.StartBit == 56 && lastSignal.Length == 8);
        }

        [Fact]
        public void SignalParserShouldReturnCorrectValues ()
        {
            string line = " SG_ cell_voltage_002 m0 : 16|8@1+ (0.01,2) [2|4.55] \"V\"  VCU";

            List<string> arguments = new List<string> { "Name", "StartBit", "Length" };

            Signal signal = SignalParser.ParseLine(line, arguments);

            Assert.True(signal.Name == "cell_voltage_002" && signal.StartBit == 16 && signal.Length == 8);
        }

        [Fact]
        public void MessageParseShouldReturnCorrectValues()
        {
            string line = "BO_ 304 Multiplexed_intel_2: 8 PDU";

            List<string> arguments = new List<string> { "MessageId", "Name" };

            Message message = MessageParser.ParseLine(line, arguments);

            Assert.True(message.Name == "Multiplexed_intel_2");
        }
    }
}
