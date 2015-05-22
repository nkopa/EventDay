package pl.helloworld;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

import android.util.Log;


public class DatabaseConnection {

	private final static String DBURL = "jdbc:...";
    private final static String DBUSER = "";
    private final static String DBPASS = "";
    private final static String DBDRIVER = "com.mysql.jdbc.Driver";
    
    private Connection connection;
    private Statement statement;
    
    private static int EventId;
    private static String Username = "";
    //chec otrzymania powiadomienia
    private static String Accept = "TAK";
    public static ArrayList<SmsDemo> List = new ArrayList<SmsDemo>(); 
    
    //wybierz Eventy, które rozpoczynaj¹ sie nastêpnego dnia
    private static String SelectEvents = "SELECT EventId, Title, DateBegin, HourBegin FROM Event WHERE CONVERT (date, DateBegin) = CONVERT (date, dateadd(day,1,GETDATE()))"; 
    //wybierz uzytkownikow zgloszonych do eventu
    private static String SelectUsers = "SELECT Username FROM JoinEvent WHERE EventId=" + EventId;   
    //pobierz dane kontaktowe uzytkownika
    private static String SelectNumber = "SELECT SmsNotification, PhoneNumber FROM UserProfile WHERE Username=" + Username;

    
    
    public void BDConnection() throws java.sql.SQLException, InstantiationException, IllegalAccessException, ClassNotFoundException{
    	
			Class.forName(DBDRIVER).newInstance();
			connection = DriverManager.getConnection(DBURL, DBUSER, DBPASS);
			statement = connection.createStatement();
			
    }

    public void BDSelect() throws SQLException{
    	
    	ResultSet Events = statement.executeQuery(SelectEvents);
        
        while (Events.next()) {
        	
        	EventId = Events.getInt(1);  	
        	ResultSet Users = statement.executeQuery(SelectUsers);
        	
        	while (Users.next()) {
        		
        		Username = Users.getString(2);
        		ResultSet Number = statement.executeQuery(SelectNumber);
    		
        		if(Number.getString(1)==Accept){
        			String PhoneNumber = Number.getString(2);
        			String Text = "Witaj " + Username + "! Wydarzenie '"+Events.getString(2)+"' rozpoczyna sie ju¿ jutro o "+ Events.getString(3) + ". Szczegó³y dostêpne na portalu EventDay ;)";
        			SmsDemo sms = new SmsDemo(PhoneNumber, Text);	
        			List.add(sms);
        		}    		
        	}
       }
    }
    
    public void BDClose() throws SQLException{
    	statement.close();
		connection.close();
    }
    
    public ArrayList<SmsDemo> CreateUsersList() throws InstantiationException, IllegalAccessException, ClassNotFoundException, SQLException{
    	BDConnection();
    	BDSelect();
    	BDClose();  	
    return List;
    }  
}