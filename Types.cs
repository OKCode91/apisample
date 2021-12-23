namespace ApiSample;
public record Post(int userId, int id, string title, string body);
public record Comment(int postId,int id,string name,string email,string body);