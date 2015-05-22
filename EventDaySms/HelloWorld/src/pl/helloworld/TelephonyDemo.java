package pl.helloworld;

import java.sql.SQLException;
import java.util.ArrayList;

import android.app.Activity;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Bundle;
import android.telephony.SmsManager;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import pl.eventDaySms.R;
import pl.helloworld.DatabaseConnection;
import pl.helloworld.SmsDemo;;

public class TelephonyDemo extends Activity {
	
	@Override
	   protected void onCreate(Bundle savedInstanceState){
						
		DatabaseConnection DB = new DatabaseConnection();
		final ArrayList<SmsDemo> UsersList = new  ArrayList<SmsDemo>();
		try {
			ArrayList<SmsDemo> TempList = DB.CreateUsersList();
			for(int i=0; i<TempList.size(); i++){
				UsersList.add(TempList.get(i));
			}		
		} catch (InstantiationException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		} catch (IllegalAccessException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		} catch (ClassNotFoundException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		} catch (SQLException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}
		
		super.onCreate(savedInstanceState);		
	    setContentView(R.layout.main);
	    Button sendBtn = (Button)findViewById(R.id.sendSmsBtn);
      
	      sendBtn.setOnClickListener(new OnClickListener(){

	         @Override
	         public void onClick(View view) {	        	 
	            
	            String nr = "";
	            String text = "";
	            int ok=0;
	            int error=0;
	            EditText addrTxt =(EditText)TelephonyDemo.this.findViewById(R.id.msgAll);
	            
				for(int i=0;i<UsersList.size();i++){
					
	            nr = UsersList.get(i).getNumber();
	            text = UsersList.get(i).getText();
	            
	            try {	
	            	sendSMS(nr, text);
     
	               ok++;
	            } catch (Exception e) {	          
	               error++;
	            }  
	            }	
				addrTxt.append("poprawnie wys³ano: "+ok+ "\nnie uda³o siê wys³aæ: "+ error+"\n");
	         }
			});	
	   }

	   @Override
	   protected void onDestroy() {
	      super.onDestroy();
	   }
	   
	   private void sendSMS(String phoneNumber, String message)
	    {    
		   final String nr = phoneNumber;
		   final String msg = message;
		   
	        String SENT = "SMS_SENT";
	        String DELIVERED = "SMS_DELIVERED";

	        ArrayList<PendingIntent> sentPI = new ArrayList<PendingIntent>();
	        PendingIntent sPI = PendingIntent.getBroadcast(this, 0, new Intent(SENT), 0);
	        ArrayList<PendingIntent> deliveredPI = new ArrayList<PendingIntent>();
	        PendingIntent dPI = PendingIntent.getBroadcast(this, 0, new Intent(DELIVERED), 0);
	        
	        //---wysylanie---
	        registerReceiver(new BroadcastReceiver(){
	        	EditText msgTxt =(EditText)TelephonyDemo.this.findViewById(R.id.msgErrors);
	            @Override
	            public void onReceive(Context arg0, Intent arg1) {
	                switch (getResultCode())
	                {
	                    case Activity.RESULT_OK:                    
	                        break;
	                    case SmsManager.RESULT_ERROR_GENERIC_FAILURE:
	                        msgTxt.append("Generic failure:[" + nr + "] [" + msg + "]\n");    
	                        break;
	                    case SmsManager.RESULT_ERROR_NO_SERVICE:
	                    	msgTxt.append("No service:		[" + nr + "] [" + msg + "]\n");
	                        break;
	                    case SmsManager.RESULT_ERROR_NULL_PDU:
	                    	msgTxt.append("Null PDU:		[" + nr + "] [" + msg + "]\n");
	                        break;
	                    case SmsManager.RESULT_ERROR_RADIO_OFF:
	                    	msgTxt.append("Radio off:		[" + nr + "] [" + msg + "]\n");
	                        break;
	                }
	            }
	        }, new IntentFilter(SENT));

	        //---dostarczenie---
	        registerReceiver(new BroadcastReceiver(){
	            @Override
	            public void onReceive(Context arg0, Intent arg1) {
	                switch (getResultCode())
	                {
	                    case Activity.RESULT_OK:
	                        break;
	                    case Activity.RESULT_CANCELED:
	                        break;                        
	                }
	            }
	        }, new IntentFilter(DELIVERED));  
	        
	        
	        //--akcja--          	        
	        try {
	            SmsManager sms = SmsManager.getDefault();
	            ArrayList<String> mSMSMessage = sms.divideMessage(message);
	            for (int i = 0; i < mSMSMessage.size(); i++) {
	                sentPI.add(i, sPI);
	                deliveredPI.add(i, dPI);
	            }
	            sms.sendMultipartTextMessage(phoneNumber, null, mSMSMessage, sentPI, deliveredPI);

	        } catch (Exception e) {
	        }	
	    }
	   
	}

	
