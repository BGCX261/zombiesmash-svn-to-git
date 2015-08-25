package com.cvut.fit.bluetoothcarhandheld;

import java.util.Random;

import android.app.Activity;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.graphics.RadialGradient;
import android.os.Bundle;
import android.text.InputType;
import android.view.Gravity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.ToggleButton;

public class TextPlay extends Activity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.text);

		Button chkCmd = (Button)findViewById(R.id.bResults);
		final ToggleButton tgButton = (ToggleButton)findViewById(R.id.tbPassword);
		final TextView tvText = (TextView)findViewById(R.id.tvResults);
		final EditText etCommands = (EditText)findViewById(R.id.etCommands);

		tgButton.setOnClickListener(new View.OnClickListener() {

			public void onClick(View v) {
				if(tgButton.isChecked())
				{
					etCommands.setInputType(InputType.TYPE_CLASS_TEXT | InputType.TYPE_TEXT_VARIATION_PASSWORD);

				}
				else
				{
					etCommands.setInputType(InputType.TYPE_CLASS_TEXT);
				}

			}
		});

		chkCmd.setOnClickListener(new View.OnClickListener() {

			public void onClick(View v) {
				String check = etCommands.getText().toString();
				if(check.contentEquals("right"))
				{
					tvText.setGravity(Gravity.RIGHT);
				}
				else if(check.contentEquals("center"))
				{
					tvText.setGravity(Gravity.CENTER);
				}
				else if(check.contentEquals("left"))
				{
					tvText.setGravity(Gravity.LEFT);
				} 
				else if(check.contentEquals("blue"))
				{
					tvText.setTextColor(Color.BLUE);
				}
				else if(check.contentEquals("WTF"))
				{
					Random crazy = new Random();
					tvText.setText("WTF");
					tvText.setTextSize(crazy.nextInt(75));
					tvText.setTextColor(Color.rgb(crazy.nextInt(255), crazy.nextInt(255), crazy.nextInt(255)));
				}
				else
				{
					tvText.setText("invalid");
					tvText.setGravity(Gravity.CENTER);
				}
					

			}
		});
	}
}
