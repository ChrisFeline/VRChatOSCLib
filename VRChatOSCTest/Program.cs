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