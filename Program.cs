using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SpeechToTextCsharp
{
    class Program
    {
       static async Task Main()
        {
            await RecognizeSpeechAsync();
        }

        static async Task RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromSubscription("your subcription key", "region(eastus, westus etc)");
            using var audioInput = AudioConfig.FromWavFileInput(@"Your directory path\sample.wav");
            using var recognizer = new SpeechRecognizer(config, audioInput);
            Console.WriteLine("Recognizing first result");
            var result = await recognizer.RecognizeOnceAsync();

            switch (result.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    Console.WriteLine($"We've recognized: {result.Text}");
                    Console.ReadLine();
                    break;
                case ResultReason.NoMatch:
                    Console.WriteLine("NOMATCH: Speech could not be recognized");
                    Console.ReadLine();
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"CANCELED: Reason = {cancellation.Reason}");
                    Console.ReadLine();
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode = {cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails = {cancellation.Reason}");
                        Console.WriteLine($"CANCELED: Did you update your subscription info?");
                        Console.ReadLine();

                    }
                    break;
            }

        }
    }
}
