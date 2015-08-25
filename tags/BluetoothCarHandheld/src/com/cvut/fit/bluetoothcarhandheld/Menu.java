package com.cvut.fit.bluetoothcarhandheld;

import android.app.ListActivity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;

public class Menu extends ListActivity {

	String classes[] = {"BluetoothCarHandheldActivity","IntroActivity", "TextPlay","Email","TextVoice"};
	
	@Override
	protected void onListItemClick(ListView l, View v, int position, long id) {
		// TODO Auto-generated method stub
		super.onListItemClick(l, v, position, id);
		String cheese = classes[position];
		try {
			Class ourClass = Class.forName("com.cvut.fit.bluetoothcarhandheld." + cheese);
			Intent intent = new Intent(Menu.this,ourClass);
			startActivity(intent);
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		
		setListAdapter(new ArrayAdapter<String>(Menu.this,android.R.layout.simple_list_item_1, classes));
	}

}
