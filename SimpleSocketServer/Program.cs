using System.Text.Json;
using SimpleSocketServer;;

ServerEngine serverEngine = new ServerEngine("127.0.0.1", 34536);
serverEngine.StartServer(); // запуск
serverEngine.AcceptClient();

string messageFromClient = serverEngine.ReceiveMessage(); //получение сообщения

Request request = JsonSerializer.Deserialize<Request>(messageFromClient);
Response response;

if (request.Command == Commands.AddAge)
{
    Cat cat = JsonSerializer.Deserialize<Cat>(request.JsonData);
    cat.Age += 2;

    response = new Response()
    {
        Status = Statuses.Ok,
        JsonData = JsonSerializer.Serialize(cat)
    };
}
else
{
    response = new Response()
    {
        Status = Statuses.UnknownCommand
    };
}

string messageToClient = JsonSerializer.Serialize(response);

serverEngine.SendMessage(messageToClient);

serverEngine.CloseClientSocket();
serverEngine.CloseServerSocket();