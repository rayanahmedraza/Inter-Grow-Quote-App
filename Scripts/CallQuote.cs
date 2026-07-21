using Godot;
using System.Text.Json;

public partial class CallQuote : Control
{
	private Button MyButton;
	private HttpRequest MyHttpRequest;
	private Label QuoteLabel;
	private Label AuthorLabel;

	public override void _Ready()
	{
		MyButton = GetNode<Button>("CallQuote");
		MyHttpRequest = GetNode<HttpRequest>("HTTPRequest");
		QuoteLabel = GetNode<Label>("QuoteLabel");
		AuthorLabel = GetNode<Label>("AuthorLabel");

		MyButton.Pressed += OnButtonPressed;
		MyHttpRequest.RequestCompleted += OnMyRequestComplete;

	}

	public void OnButtonPressed()
	{
		
		MyHttpRequest.Request("https://dummyjson.com/quotes/random");
		
	}
	public void OnMyRequestComplete(long Result, long ResponseCode, string[] headers, byte[] body)
	{
		string Response = System.Text.Encoding.UTF8.GetString(body);
		QuoteData Quote = JsonSerializer.Deserialize<QuoteData>(Response);

		QuoteLabel.Text = Quote.quote;
		AuthorLabel.Text = $"-{Quote.author}";
	}
}

public class QuoteData
{
	public int id {get; set;}
	public string quote {get; set;}
	public string author{get; set;}
}
