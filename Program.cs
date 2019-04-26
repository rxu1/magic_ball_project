using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace MagicBall
{
  class Program
  {
    public static async Task RecognizeSpeechAsync()
    {
      // Creates an instance of a speech config with specified subscription key and service region.
      // Replace with your own subscription key and service region (e.g., "westus").
      var config = SpeechConfig.FromSubscription("f961d606be3a4bc38f2a0a1ef0cc0d5a", "westus");

      // Creates a speech recognizer.
      using (var recognizer = new SpeechRecognizer(config))
      {
        Console.WriteLine("Say something...");

        // Starts speech recognition, and returns after a single utterance is recognized. The end of a
        // single utterance is determined by listening for silence at the end or until a maximum of 15
        // seconds of audio is processed.  The task returns the recognition text as result. 
        // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
        // shot recognition like command or query. 
        // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
        var result = await recognizer.RecognizeOnceAsync();

        // Checks result.
        if (result.Reason == ResultReason.RecognizedSpeech)
        {
          Console.WriteLine($"We recognized: {result.Text}");
        }
        else if (result.Reason == ResultReason.NoMatch)
        {
          Console.WriteLine($"NOMATCH: Speech could not be recognized.");
        }
        else if (result.Reason == ResultReason.Canceled)
        {
          var cancellation = CancellationDetails.FromResult(result);
          Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

          if (cancellation.Reason == CancellationReason.Error)
          {
            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
            Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
            Console.WriteLine($"CANCELED: Did you update the subscription info?");
          }
        }             
      }
    }

    public static async Task SynthesisToSpeakerAsync()
        {
            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            // The default language is "en-us".
            var config = SpeechConfig.FromSubscription("YourSubscriptionKey", "YourServiceRegion");

            // Creates a speech synthesizer using speaker as audio output.
            using (var synthesizer = new SpeechSynthesizer(config))
            {
                // Receive a text from console input and synthesize it to speaker.
                Console.WriteLine("Type some text that you want to speak...");
                Console.Write("> ");
                string text = Console.ReadLine();

                using (var result = await synthesizer.SpeakTextAsync(text))
                {
                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
                    }
                    else if (result.Reason == ResultReason.Canceled)
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                        if (cancellation.Reason == CancellationReason.Error)
                        {
                            Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                            Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                            Console.WriteLine($"CANCELED: Did you update the subscription info?");
                        }
                    }
                }

                // This is to give some time for the speaker to finish playing back the audio
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

    static void Main()
    {
      RecognizeSpeechAsync().Wait();
      SynthesisToSpeakerAsync().Wait();
      Console.WriteLine("Please press a key to continue.");
      Console.ReadLine();
    }               
  }
}