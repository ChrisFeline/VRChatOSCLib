# VRChatOSC Library for C#
## A simple, straightforward OSC library for VRChat created in .NET Core 6, CSharp

## Usage
##### These examples are for usage demonstrations... do not just copy-paste.
###### Or do. I am a programmer, not a cop.

### Namespace
```cs
using VRChatOSCLib;
```
### Initializing
#### You can use any of these methods to initialize the class.
```cs
VRChatOSC vrcOsc = new VRChatOSC(); // Doesn't connect, use .Connect() after
            .... = new VRChatOSC(true);// Connect and use default remote port 9000
            .... = new VRChatOSC(8901); // Connect and use custom remote port
            .... = new VRChatOSC("192.168.1.X", 9000); // Connect and Use custom IP from string and custom port
            .... = new VRChatOSC(IPAddress ipAddress, 9000); // Connect and Use custom IP from IPAddress class

// If no parameters are passed to the constructor, you will need to connect the client manually. You can do this using the .Connect() method.
vrcOsc.Connect(); // Uses default remote port 9000
vrcOsc.Connect(8901); // Use custom remote port
vrcOsc.Connect("192.168.1.X", 9000); // Use custom IP from string and custom port
vrcOsc.Connect(IPAddress ipAddress, 9000); // Use custom IP from IPAddress class
```
## Examples
```cs
using VRChatOSCLib;

internal class Program
{
    static VRChatOSC osc = new VRChatOSC();

    static async Task Main(string[] args)
    {
        // Connect. Port number is optional
        osc.Connect(9000);

        // Asyncn't
        // Send to a specific address
        osc.SendTo("/test/lib/float", 0.5f);


        // Send to an avatar parameter
        // This will automatically send it to the proper path
        osc.SendParameter("GlassesToggle", true); // Boolean sent as "/avatar/parameters/GlassesToggle"
        osc.SendParameter("GlassesColor", 0.5f); // Float sent as "/avatar/parameters/GlassesColor"
        osc.SendParameter("UltimateAnswer", 42); // Integer sent as "/avatar/parameters/UltimateAnswer"


        // Send supported inputs like buttons and Axes to VRChat
        osc.SendInput(VRCButton.Jump, true); // Jump sent as "/input/Jump/" 1
        osc.SendInput(VRCButton.Jump, false); // Jump sent as "/input/Jump/" 0

        osc.SendInput(VRCAxes.Vertical, 0.42f); // Vertical Axes sent as "/input/Vertical/" 0.42f


        // Send Chatbox Messages
        osc.SendChatbox("Hello World"); // Sends the ASCII string to the Keyboard
        osc.SendChatbox("Hello World", true); // Bypass keyboard and sends string to Chatbox

        osc.SendChatbox(true); // Set typing indicator ON
        osc.SendChatbox(false); // Set typing indicator OFF


        // Async aka Awaitable methods
        await osc.SendToAsync("/test/lib/async", true);
        await osc.SendParameterAsync("AsyncRocks", true);
        await osc.SendInputAsync(VRCButton.Run, true);
        await osc.SendChatboxAsync("Hello World", true);


        // Listen for incoming messages
        osc.Listen(); // Listen on default port 9001
        osc.Listen(9042); // Listen on custom port
        osc.Listen(9001, 1024); // Use custom port and custom buffer length

        // Subscribe to incoming messages
        osc.OnMessage += OnMessageReceived;

        Console.ReadKey(true); // :P
    }

    static void OnMessageReceived(object? source, VRCMessage message)
    {
        // Full address from message, example "/avatar/parameters/CatToggle"
        string address = message.Address;
        // Shortened path from the address, example "/avatar/parameters/"
        string path = message.Path;

        // True if this message is an avatar parameter of any kind
        bool isParameter = message.IsParameter;

        // Message type (Unknown|DefaultParameter|AvatarParameter|AvatarChange)
        VRCMessage.MessageType messageType = message.Type;

        // Retrieve the first value object contained in this message
        object? value = message.GetValue(); // Can return default
        float val = message.GetValue<float>(); // Alternative with generic type

        val = message.GetValueAt<float>(0); // Get argument value by index
        value = message[0]; // Same with indexer

        // Print this message with fancy colors for debugging
        message.Print();
    }
}
```
  
## Credits

- [OscCore](https://github.com/tilde-love/osc-core)
- There's probably many libraries out there for VRC, also give them a try. This was originally a personal project for personal use, but I decided to share it.