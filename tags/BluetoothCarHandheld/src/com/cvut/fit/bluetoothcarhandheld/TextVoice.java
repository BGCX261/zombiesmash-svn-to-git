package com.cvut.fit.bluetoothcarhandheld;

import java.util.Locale;
import java.util.Random;

import android.app.Activity;
import android.os.Bundle;
import android.speech.tts.TextToSpeech;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

public class TextVoice extends Activity implements OnClickListener {
	
	EditText textforSpeech;
	String whatShouldISay;

	static final String[] texts = {
		"What's up Gangsters!", "You smell!", "I have a crush on Travis", "Obama is a piece of shit man."
	};
	TextToSpeech tts;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.textvoice);
		Button b = (Button)findViewById(R.id.bTextToVoice);
		textforSpeech = (EditText)findViewById(R.id.etText);
		b.setOnClickListener(this);
		tts = new TextToSpeech(TextVoice.this, new TextToSpeech.OnInitListener() {
			
			public void onInit(int status) {
				// TODO Auto-generated method stub
				if (status != TextToSpeech.ERROR){
					tts.setLanguage(Locale.US);
				}
			}
		});
	}

	@Override
	protected void onPause() {
		// TODO Auto-generated method stub
		if(tts !=null){
			tts.stop();
			tts.shutdown();
		}
		super.onPause();
	}

	public void onClick(View arg0) {
		// TODO Auto-generated method stub
		Random r = new Random();
		whatShouldISay = textforSpeech.getText().toString();
		tts.speak(whatShouldISay, TextToSpeech.QUEUE_FLUSH, null);
	}
}
