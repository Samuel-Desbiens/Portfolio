package com.example.tp2etu.view;

import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.os.Parcelable;
import android.view.View;
import android.widget.Button;

import androidx.appcompat.app.AppCompatActivity;

import com.example.tp2etu.data.Humidity;
import com.example.tp2etu.R;
import com.example.tp2etu.data.SensorData;
import com.example.tp2etu.database.MyDatabaseFactory;
import com.example.tp2etu.model.SensorValue;
import com.example.tp2etu.data.Temperature;
import com.example.tp2etu.repository.HumidityRepository;
import com.example.tp2etu.repository.TemperatureRepository;
import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.android.material.snackbar.Snackbar;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.Arrays;

public class MainActivity extends AppCompatActivity {

    private String json;
    private Temperature[] temperatures;
    private Humidity[] humidities;
    private Parcelable[] parcelableArray;
    private SensorValue[] sensorValueArray;
    private SensorData sensorData;
    private Button tempButton;
    private Button humiButton;
    private MyDatabaseFactory databaseFactory;
    private SQLiteDatabase database;
    private HumidityRepository repoHumidity;
    private TemperatureRepository repoTemperature;

    public MainActivity() {
    }


    /**
     * Gère l'intent implicite et les changement d'orientation.
     * @param savedInstanceState Le bundle de l'application.
     */
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        databaseFactory = new MyDatabaseFactory(this);
        repoHumidity = new HumidityRepository(databaseFactory.getWritableDatabase());
        repoTemperature = new TemperatureRepository(databaseFactory.getWritableDatabase());

        humiButton = findViewById(R.id.humidity);
        tempButton = findViewById(R.id.temperature);


        Intent intent = getIntent();
        if(intent != null) {
            if ("text/plain".equals(intent.getType())) {
                String text = intent.getStringExtra(intent.EXTRA_TEXT);
                if (text == "HUMIDITY_ID") {
                    humiButton.performClick();
                } else if (text == "TEMPERATURE_ID") {
                    tempButton.performClick();
                }
            }
        }


        if(savedInstanceState != null)
        {
            if(!savedInstanceState.isEmpty())
            {
                //Inspirer de :https://stackoverflow.com/questions/10071502/read-writing-arrays-of-parcelable-objects
                if(savedInstanceState.containsKey("TEMPERATURE"))
                {
                    parcelableArray  = savedInstanceState.getParcelableArray("TEMPERATURE");
                    temperatures = null;
                    if(parcelableArray != null)
                    {
                        temperatures = Arrays.copyOf(parcelableArray,parcelableArray.length,Temperature[].class);
                    }
                }
                else if(savedInstanceState.containsKey("HUMIDITY"))
                {
                    parcelableArray = savedInstanceState.getParcelableArray("HUMIDITY");
                    humidities = null;
                    if(parcelableArray != null)
                    {
                        humidities = Arrays.copyOf(parcelableArray,parcelableArray.length,Humidity[].class);
                    }
                }
            }
        }

    }

    /**
     * Gère la sauvgarde des données après un changement d'orientation et le retour de l'application du "background".
     * @param outState Le bundle qui sauvegarde les données pour le retour "on create".
     */
    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);

        if(temperatures != null)
        {
            outState.putParcelableArray("TEMPERATURE", temperatures);
        }
        if(humidities != null)
        {
            outState.putParcelableArray("HUMIDITY", humidities);
        }
    }


    /**
     * Gère les actions nécéssaire au changement d'activité après l'appuie du bouton "Start Humidity".
     * @param view La vue comprenant les controles de l'application.
     */
    public void humidityOnClick(View view)
    {
        sensorData = LoadJSON(R.raw.humidity);
        sensorValueArray = new SensorValue[sensorData.getValues().length];
        sensorValueArray = sensorData.getValues();
        humidities = new Humidity[sensorValueArray.length];
        for(int i = 0;i<humidities.length;i++)
        {
            humidities[i] = new Humidity();
            humidities[i].setTimestamps(sensorValueArray[i].getTimeStamp());
            humidities[i].setValue(sensorValueArray[i].getValue());
        }
        Intent intent = new Intent(this, HumidityActivity.class);
        intent.putExtra("HUMIDITY", humidities);

        startActivity(intent);
    }

    /**
     * Gère les actions nécéssaire au changement d'activité après l'appuie du bouton "Start Temperature".
     * @param view La vue comprenant les controles de l'application.
     */
    public void temperatureOnClick(View view)
    {
        sensorData = LoadJSON(R.raw.temperature);
        sensorValueArray = new SensorValue[sensorData.getValues().length];
        sensorValueArray = sensorData.getValues();
        temperatures = new Temperature[sensorValueArray.length];
        for(int i = 0;i<temperatures.length;i++)
        {
            temperatures[i] = new Temperature();
            temperatures[i].setTimestamps(sensorValueArray[i].getTimeStamp());
            temperatures[i].setValue(sensorValueArray[i].getValue());
        }
        Intent intent = new Intent(this, TemperatureActivity.class);
        intent.putExtra("TEMPERATURE", temperatures);

        startActivity(intent);
    }

    /**
     * Gère l'envoie vers l'activité statistique.
     * @param view La vue actuel.
     */
    public void statsOnClick(View view)
    {
        Intent intent = new Intent(this, StatsActivity.class);
        startActivity(intent);
    }

    /**
     * S'occupe du chargement des données avec les " SensorData " provenant des fichier .json.
     * @param ressourceId Le fichier à ouvrir dépendant de quel "Sensor" est demandé.
     * @return Le SensorData contenant les données du sensor dans un "array" de valeur.
     */
    public SensorData LoadJSON(int ressourceId)
    {
        SensorData sd;
        json = "";
        InputStream is = this.getResources().openRawResource(ressourceId);
        BufferedReader br = new BufferedReader(new InputStreamReader(is));
        String readLine = null;

        try
        {
            while((readLine = br.readLine()) != null)
            {
                json += readLine;
            }
        }
        catch(IOException e)
        {
            e.printStackTrace();
        }

        try {
            ObjectMapper objectMapper = new ObjectMapper();
            sd = objectMapper.readValue(json,SensorData.class);
        } catch (JsonProcessingException e) {
            e.printStackTrace();
            return null;
        }
        return sd;
    }



}
