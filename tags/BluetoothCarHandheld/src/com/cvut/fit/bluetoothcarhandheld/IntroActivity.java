package com.cvut.fit.bluetoothcarhandheld;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

public class IntroActivity extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.intro);

		Thread timer = new Thread() {

			public void run() {
				try {
					sleep(5000);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
				finally{
					
					Intent start = new Intent("com.cvut.fit.MENU");
					startActivity(start);
					
				}

			}

		};
		
		timer.start();

	}

}
