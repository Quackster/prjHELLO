# prjHELLO
My first ever attempt to write a V1 Habbo Hotel server, originally written in 2011.

**Summary:** Project HELLO is a server I created to challenge myself after noticing that "v1 servers" were dead in another forum. This server, while very basic, is great for beginners learning C#.

Since many are more interested in the features than the backstory, here they are:

**Google Code SVN:** project-handshake - Revision 2: /trunk

**CHANGE LOG:**
- **6 - 27 - 2011**
  - Register works, download from SVN at: [project-handshake.googlecode.com/svn/trunk/r2/](http://project-handshake.googlecode.com/svn/trunk/r2/)
  - More public rooms! (Thanks to SrTetris): [forum.ragezone.com/6393516-post18.html](http://forum.ragezone.com/6393516-post18.html)
    - MySQL (Class stolen from Ion).
    - Socket TCP Server (Website: [C# Tutorial - Simple Threaded TCP Server | Switch on the Code](https://www.switchonthecode.com/tutorials/csharp-tutorial-simple-threaded-tcp-server))
    - Habbo Hotel V1 protocol
    - Unique V1 packet handling (Thor inspired)

**Habbo Features:**
- Login from MySQL - checks features
- Packet Handling (Unique for C# V1 servers) example:

    ```csharp
    fuseMessage = new serverMessage("SYSTEMBROADCAST");
    fuseMessage.AppendString("Wrong username or password.");
    NewSocket.SendData(clientStream, fuseMessage);
    ```

- Loads users' credits when logging in.
- Sends USEROBJECT on login for room entering (therefore being able to see figure).
- Loads console motto
- Loads private rooms from database
- Loads public rooms from database.
- No more switch cases, uses voids instead! (delegates) example:

    ```csharp
    public void VERSIONCHECK()
    {
        fuseMessage = new serverMessage("ENCRYPTION_OFF");
        NewSocket.SendData(clientStream, fuseMessage);

        fuseMessage = new serverMessage("SECRET_KEY");
        fuseMessage.AppendInteger(1337);
        NewSocket.SendData(clientStream, fuseMessage);
    }
    ```

**Why is the project called HELLO?**

The short answer is that HELLO is today's <policy... handshake packet. V1 had no encryption, so why not name a server that has so much similarity to the protocol, cool huh? :D
