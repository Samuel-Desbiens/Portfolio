package com.example.tp2etu.view;

import android.app.Activity;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.os.Handler;
import android.os.Parcelable;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import androidx.activity.OnBackPressedCallback;

import com.example.tp2etu.data.Humidity;
import com.example.tp2etu.R;
import com.example.tp2etu.database.MyDatabaseFactory;
import com.example.tp2etu.model.ModelControllerHumidity;
import com.example.tp2etu.repository.HumidityRepository;
import com.example.tp2etu.repository.TemperatureRepository;
import com.google.android.material.snackbar.Snackbar;
import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.LegendRenderer;
import com.jjoe64.graphview.series.BarGraphSeries;
import com.jjoe64.graphview.series.DataPoint;

import java.util.ArrayList;
import java.util.Arrays;


public class HumidityActivity extends Activity
{
    ModelControllerHumidity MCH;
    Humidity[] humidities;
    Parcelable[] parcelableArray;
    GraphView graph;
    Spinner spinner;
    EditText zoneValue;
    TextView HlValue;
    TextView LlValue;
    private MyDatabaseFactory databaseFactory;
    private SQLiteDatabase database;
    private HumidityRepository repoHumidity;

    /**
     * Permet de gérer le lancement de l'activité et la reprise de celle-ci ajoute les données de base au graphique.
     * @param savedInstanceState Le bundle avec les données sauvgarder ou reçu de d'autre activité.
     */
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_humidity);

        databaseFactory = new MyDatabaseFactory(this);
        repoHumidity = new HumidityRepository(databaseFactory.getReadableDatabase());

        graph = findViewById(R.id.Graph);
        spinner = findViewById(R.id.spinner);
        zoneValue = findViewById(R.id.zoneValue);
        HlValue = findViewById(R.id.HLValue);
        LlValue = findViewById(R.id.LLValue);



        Intent intent = getIntent();
        if(intent != null)
        {
            parcelableArray = intent.getParcelableArrayExtra("HUMIDITY");
            humidities = null;
            if(parcelableArray != null)
            {
                humidities = Arrays.copyOf(parcelableArray,parcelableArray.length,Humidity[].class);
            }
        }

        MCH = new ModelControllerHumidity(humidities);

        zoneValue.setText(String.valueOf(MCH.getNbZones()));



        ArrayList<String> arraylist = new ArrayList<>();
        DataPoint[] points = new DataPoint[MCH.getNbZones()];
        for(int i = 0;i<points.length;i++)
        {
            points[i] = new DataPoint(i,MCH.getUpperLimit(i)-MCH.getLowerLimit(i));
            arraylist.add(String.valueOf(i));
        }
        ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(this,android.R.layout.simple_spinner_dropdown_item,arraylist);
        arrayAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(arrayAdapter);
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener()
    {
        /**
         * Permet d'agir en fonction de la selection du spinner
         * @param adapterView Aucune idée.
         * @param view La vue actuel du spinner.
         * @param i La position de la selection.
         * @param l L'index de la selection.
         */
        @Override
        public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l)
        {
            String futurInt = adapterView.getItemAtPosition(i).toString();
            MCH.setSelectedZone(Integer.parseInt(futurInt));
            HlValue.setText(String.valueOf(MCH.getUpperLimit(MCH.getSelectedZone())));
            LlValue.setText(String.valueOf(MCH.getLowerLimit(MCH.getSelectedZone())));
        }

        /**
         * Fonction Servant a gerer la non selection d'un menu (obligatoire pour le onItemSelectedListener
         * @param adapterView Aucune idée.
         */
        @Override
        public void onNothingSelected(AdapterView<?> adapterView) {

        }
    });

        BarGraphSeries<DataPoint> series = new BarGraphSeries<>(points);

        series.setTitle("Humidity");

        graph.getViewport().setMinY(0);
        double maxY = 0;
        for(int i = 0 ; i<MCH.getNbZones();i++)
        {
            if(MCH.getUpperLimit(i)-MCH.getLowerLimit(i) > maxY)
            {
                maxY = MCH.getUpperLimit(i)-MCH.getLowerLimit(i);
            }
        }
        graph.getViewport().setMaxY(maxY);
        graph.getViewport().setMinX(0);
        graph.getViewport().setMaxX(MCH.getNbZones());

        graph.getLegendRenderer().setVisible(true);
        graph.getLegendRenderer().setAlign(LegendRenderer.LegendAlign.MIDDLE);

        graph.addSeries(series);
    }

    /**
     * Permet la sauvgarde des données en cas de changement d'orientation ou d'un retour du "Background".
     * @param outState Le bundle dans lequel on vas sauvgarder les données.
     */
    @Override
    protected void onSaveInstanceState(Bundle outState)
    {
        super.onSaveInstanceState(outState);

        if(humidities != null)
        {
            outState.putParcelableArray("HUMIDITY", humidities);
        }
    }

    /**
     * Permet de gérer le bouton apply changeant les données.
     * @param view La vue actuelle.
     */
    public void updateZoneOnClick(View view)
    {
        try
        {
            MCH.setNbZones(Integer.parseInt(zoneValue.getText().toString()));
        }
        catch (NumberFormatException e)
        {
            e.printStackTrace();
        }
        int dataEachZone = humidities.length / MCH.getNbZones();
        DataPoint[] points = new DataPoint[MCH.getNbZones()];
        for(int i = 0;i<points.length;i++)
        {
            points[i] = new DataPoint(i,MCH.getUpperLimit(i)-MCH.getLowerLimit(i));
        }
        MCH.updateZones(dataEachZone);

        graph.removeAllSeries();

        points = new DataPoint[MCH.getNbZones()];
        for(int i = 0;i<points.length;i++)
        {
            points[i] = new DataPoint(i,MCH.getUpperLimit(i)-MCH.getLowerLimit(i));
        }
        BarGraphSeries<DataPoint> series = new BarGraphSeries<>(points);

        series.setTitle("Humidity");

        double maxY = 0;
        double minY = 0;
        for(int i = 0 ; i<MCH.getNbZones();i++)
        {
            if(MCH.getUpperLimit(i)-MCH.getLowerLimit(i) > maxY)
            {
                maxY = MCH.getUpperLimit(i)-MCH.getLowerLimit(i);
            }
            else if (MCH.getUpperLimit(i)-MCH.getLowerLimit(i) < minY)
            {
                minY = MCH.getUpperLimit(i)-MCH.getLowerLimit(i);
            }
        }
        graph.getViewport().setMinY(minY);
        graph.getViewport().setMaxY(maxY);
        graph.getViewport().setMinX(0);
        graph.getViewport().setMaxX(MCH.getNbZones());

        graph.addSeries(series);
    }

    /**
     * Gère le boutton de renvoie vers l'activité de base.
     * @param view la vue actuelle.
     */
    public void BackOnClick(View view)
    {
        if(repoHumidity.insert(humidities))
        {
            Snackbar.make(getWindow().getDecorView().getRootView(), R.string.save, Snackbar.LENGTH_LONG).show();
            //inspirer de https://stackoverflow.com/questions/17237287/how-can-i-wait-for-10-second-without-locking-application-ui-in-android
            Handler handler = new Handler();
            handler.postDelayed(new Runnable() {
                public void run() {
                    finish();
                }
            },1000);
        }
        else
        {
            Snackbar.make(getWindow().getDecorView().getRootView(), R.string.nosave, Snackbar.LENGTH_LONG).show();
            //inspirer de https://stackoverflow.com/questions/17237287/how-can-i-wait-for-10-second-without-locking-application-ui-in-android
            Handler handler = new Handler();
            handler.postDelayed(new Runnable() {
                public void run() {
                    finish();
                }
            },1000);
        }
    }
}
