using System.Collections.Generic;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenAIText : MonoBehaviour
{
    const string KEY = "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";
    
    [SerializeField] TMP_Text botText;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button sendButton;
    
    OpenAIAPI openAIAPI;
    List<ChatMessage> chatMessages = new List<ChatMessage>();
    
    void Start()
    {
        openAIAPI = new OpenAIAPI(KEY);
        StartConversation();
    }
    
    void StartConversation()
    {
        chatMessages = new List<ChatMessage>
        {
            new ChatMessage(ChatMessageRole.System, "Your name Liva, virtual lab assistant for students, only answer questions that states \"about any scientific experiment, what is virtualiva? virtualiva is 3D labs with ai assistant can help you to do your virtual experience by building your own lab and choose the tools you need. virtualiva is your organization \" deny any other questions , don't answer any question that not related to any scientific experiment, keep your answers short and to the point.") 
        };
        
        botText.text = "Hello, I am LIVA your virtual lab assistant, you can ask me any questions you want.";
        inputField.text = "";        
    }

    public async void GetResponse()
    {
        if (inputField.text == "") return;
        sendButton.interactable = false;
        
        botText.text = "Thinking...";
        
        // 
        var userMsg = new ChatMessage(ChatMessageRole.User, inputField.text);
        if (userMsg.Content.Length > 100)
        {
            userMsg.Content = userMsg.Content.Substring(0, 100);
        }
        
        chatMessages.Add(userMsg);
        
        Debug.Log(string.Format("{0} {1}", userMsg.Role, userMsg.Content));
        
        inputField.text = "";
        
        bool isDone = false;
        string errorMessage = "";
        float tryCount = 0;

        while (isDone == false && tryCount < 5)
        {
            try
            {
                var response = await openAIAPI.Chat.CreateChatCompletionAsync(new ChatRequest()
                {
                    Model = Model.ChatGPTTurbo0301,
                    Temperature = 0.1,
                    MaxTokens = 50,
                    Messages = chatMessages
                });

                ChatMessage responseMessage =
                    new ChatMessage(response.Choices[0].Message.Role, response.Choices[0].Message.Content);
                chatMessages.Add(responseMessage);
                Debug.Log(string.Format("{0} {1}", responseMessage.Role, responseMessage.Content));

                botText.text = responseMessage.Content;
                sendButton.interactable = true;
                isDone = true;
            }
            catch (System.Exception e)
            {
                errorMessage = e.Message;
                Debug.Log(errorMessage);
                isDone = false;
                tryCount++;
                
                // Sleep for 1 second
                await System.Threading.Tasks.Task.Delay(1000);
            }
        }
        
        if (tryCount >= 5)
        {
            botText.text = "Sorry, I am having trouble understanding you.";
            sendButton.interactable = true;
        }
    }
}
