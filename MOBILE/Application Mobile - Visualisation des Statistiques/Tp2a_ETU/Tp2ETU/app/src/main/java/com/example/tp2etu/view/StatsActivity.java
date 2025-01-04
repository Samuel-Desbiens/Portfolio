package com.example.tp2etu.view;

import android.app.Activity;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.example.tp2etu.R;
import com.example.tp2etu.data.Humidity;
import com.example.tp2etu.data.Temperature;
import com.example.tp2etu.database.MyDatabaseFactory;
import com.example.tp2etu.repository.HumidityRepository;
import com.example.tp2etu.repository.TemperatureRepository;
import com.jjoe64.graphview.series.DataPoint;

import java.util.ArrayList;

public class StatsActivity extends AppCompatActivity {
    private MyDatabaseFactory databaseFactory;
    private SQLiteDatabase database;
    private Spinner spinner;
    private TextView lowLimitValue;
    private TextView highLimitValue;
    private HumidityRepository repoHumidity;
    private TemperatureRepository repoTemperature;

    /**
     * Gère la création de l'activité et s'occupe qu'on ait accès au contrôle de la vue
     *
     * @param savedInstanceState Le Bundle transférer d'une activité a l'autre.
     */
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_stats);

        databaseFactory = new MyDatabaseFactory(this);
        repoHumidity = new HumidityRepository(databaseFactory.getReadableDatabase());
        repoTemperature = new TemperatureRepository(databaseFactory.getReadableDatabase());

        spinner = findViewById(R.id.spinner);
        lowLimitValue = findViewById(R.id.llAnswer);
        highLimitValue = findViewById(R.id.hlAnswer);

    }

    /**
     * Gère le boutton de retour à l'arrière.
     *
     * @param view La vue du controlle.
     */
    public void BackOnClick(View view) {
        finish();
    }

    /**
     * Gère la demande de voir les données de température dans la base de donnée.
     *
     * @param view La vue ou ce trouve le contrôle.
     */
    public void onBtnTemperature(View view) {
        ArrayList<String> arraylist = new ArrayList<>();
        final Temperature[] temperatures = repoTemperature.findAll();
        if (temperatures != null) {
            for (int i = 0; i < temperatures.length; i++) {
                if (temperatures[i].getId() == 0) {
                    arraylist.add(temperatures[i].getDate());
                }
            }
            ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_dropdown_item, arraylist);
            arrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            spinner.setAdapter(arrayAdapter);
            spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                /**
                 * Permet d'agir en fonction de la selection du spinner
                 * @param adapterView Aucune idée.
                 * @param view La vue actuel du spinner.
                 * @param i La position de la selection.
                 * @param l L'index de la selection.
                 */
                @Override
                public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                    String low = String.valueOf(temperatures[i].getMin());
                    String high = String.valueOf(temperatures[i].getMax());
                    lowLimitValue.setText(low);
                    highLimitValue.setText(high);
                }

                /**
                 * Fonction Servant a gerer la non selection d'un menu (obligatoire pour le onItemSelectedListener
                 * @param adapterView Aucune idée.
                 */
                @Override
                public void onNothingSelected(AdapterView<?> adapterView) {

                }
            });
        }

    }

    /**
     * Gère la demande de voir les données d'humidité dans la base de donnée.
     *
     * @param view La vue ou ce trouve le contrôle.
     */
    public void onBtnHumidity(View view) {
        ArrayList<String> arraylist = new ArrayList<>();
        final Humidity[] humidities = repoHumidity.findAll();
        if (humidities != null) {
            for (int i = 0; i < humidities.length; i++) {
                if (humidities[i].getId() == 1) {
                    arraylist.add(humidities[i].getDate());
                }
            }
            ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(this, android.R.layout.simple_spinner_dropdown_item, arraylist);
            arrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            spinner.setAdapter(arrayAdapter);
            spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                /**
                 * Permet d'agir en fonction de la selection du spinner
                 * @param adapterView Aucune idée.
                 * @param view La vue actuel du spinner.
                 * @param i La position de la selection.
                 * @param l L'index de la selection.
                 */
                @Override
                public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                    String low = String.valueOf(humidities[i].getMin());
                    String high = String.valueOf(humidities[i].getMax());
                    lowLimitValue.setText(low);
                    highLimitValue.setText(high);
                }

                /**
                 * Fonction Servant a gerer la non selection d'un menu (obligatoire pour le onItemSelectedListener
                 * @param adapterView Aucune idée.
                 */
                @Override
                public void onNothingSelected(AdapterView<?> adapterView) {

                }
            });
        }

    }
}

