package pl.helloworld;

public class SmsDemo {
	
	private String number;
	private String text;
	
	public SmsDemo() {
	}
	
	public SmsDemo(String number, String text) {
		this.number=number;
		this.text=text;
	}

	String getNumber(){
		return this.number;
	}
	
	void setNumber(String number){
		this.number=number;
	}
	
	String getText(){
		return this.text;
	}
	
	void setText(String text){
		this.text=text;
	}
	
}
