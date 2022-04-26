public interface IChatable
{
	void onChatFromMe(string text, string to);
	void onCancelChat();
	void onChatInPickMob(string text, string to);
	void onChatInXmap(string text, string to);
}
