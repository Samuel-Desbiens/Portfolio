package com.example.tp2etu.view;

import android.app.Activity;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.os.Handler;
import android.os.Parcelable;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

import com.example.tp2etu.R;
import com.example.tp2etu.database.MyDatabaseFactory;
import com.example.tp2etu.model.ModelControllerTemperature;
import com.example.tp2etu.data.Temperature;
import com.example.tp2etu.repository.HumidityRepository;
import com.example.tp2etu.repository.TemperatureRepository;
import com.google.android.material.snackbar.Snackbar;
import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.LegendRenderer;
import com.jjoe64.graphview.series.DataPoint;
import com.jjoe64.graphview.series.LineGraphSeries;

import java.util.Arrays;

public class TemperatureActivity extends Activity
{
    ModelControllerTemperature MCT;
    Temperature[] temperatures;
    Parcelable[] parcelableArray;
    GraphView graph;
    LineGraphSeries<DataPoint> seriesMax;
    DataPoint[] pointsMax;
    LineGraphSeries<DataPoint> seriesMin;
    DataPoint[] pointsMin;
    LineGraphSeries<DataPoint> seriesT;
    DataPoint[] pointsT;
    Button plusHigh;
    Button minusHigh;
    Button plusLow;
    Button minusLow;
    TextView HlValue;
    TextView LlValue;
    private MyDatabaseFactory databaseFactory;
    private SQLiteDatabase database;
    private TemperatureRepository repoTemperature;

    /**
     * Gère le lancement de l'application ,les changements d'orientation et les retours du "Background" Mets aussi
     * les données de base dans le graphique.
     * @param savedInstanceState Le bundle contenant les informations sauvgardées.
     */
    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_temperature);

        databaseFactory = new MyDatabaseFactory(this);
        repoTemperature = new TemperatureRepository(databaseFactory.getReadableDatabase());

        graph = findViewById(R.id.Graph);
        plusHigh = findViewById(R.id.AddHL);
        minusHigh = findViewById(R.id.SubHL);
        plusLow = findViewById(R.id.AddLL);
        minusLow = findViewById(R.id.SubLL);
        HlValue = findViewById(R.id.HLValue);
        LlValue = findViewById(R.id.LLValue);

        Intent intent = getIntent();
        if(intent != null)
        {
            parcelableArray = intent.getParcelableArrayExtra("TEMPERATURE");
            temperatures = null;
            if(parcelableArray != null)
            {
                temperatures = Arrays.copyOf(parcelableArray,parcelableArray.length,Temperature[].class);
            }
        }

        pointsT = new DataPoint[temperatures.length];
        pointsMin = new DataPoint[pointsT.length];
        pointsMax = new DataPoint[pointsT.length];

        double minX = 0;
        double maxX = 0;
        for(int i =0;i<pointsT.length;i++)
        {
            pointsT[i] = new DataPoint(temperatures[i].getTimestamp(),temperatures[i].getValue());
            if(MCT == null)
            {
                if(i == 0)
                {
                    minX = temperatures[i].getValue();
                    maxX = temperatures[i].getValue();
                }
                else
                {
                    if(temperatures[i].getValue() < minX)
                    {
                        minX = temperatures[i].getValue();
                    }
                    else if(temperatures[i].getValue() > maxX)
                    {
                        maxX = temperatures[i].getValue();
                    }
                }
            }
        }
        if(MCT != null)
        {
            minX = MCT.getLowLimit();
            maxX = MCT.getHighLimit();
        }
        else
        {
            MCT = new ModelControllerTemperature(maxX,minX);
        }

        for(int o = 0 ; o<pointsMax.length;o++)
        {
            pointsMax[o] = new DataPoint(o,maxX);
            pointsMin[o] = new DataPoint(o,minX);
        }
        seriesMax = new LineGraphSeries<>(pointsMax);
        seriesT = new LineGraphSeries<>(pointsT);
        seriesMin = new LineGraphSeries<>(pointsMin);

        seriesT.setDrawDataPoints(true);

        seriesMax.setColor(getResources().getColor(R.color.blue));
        seriesT.setColor(getResources().getColor(R.color.black));
        seriesMin.setColor(getResources().getColor(R.color.red));

        seriesMax.setTitle(getResources().getString(R.string.high_limit));
        seriesT.setTitle(getResources().getString(R.string.temperature));
        seriesMin.setTitle(getResources().getString(R.string.low_limit));

        graph.addSeries(seriesMax);
        graph.addSeries(seriesT);
        graph.addSeries(seriesMin);

        graph.getViewport().setXAxisBoundsManual(true);
        graph.getViewport().setMinX(0);
        graph.getViewport().setMaxX(pointsT.length);

        graph.getViewport().setYAxisBoundsManual(true);
        graph.getViewport().setMinY(minX);
        graph.getViewport().setMaxY(maxX);

        graph.getLegendRenderer().setVisible(true);
        graph.getLegendRenderer().setAlign(LegendRenderer.LegendAlign.MIDDLE);

        HlValue.setText(String.valueOf(MCT.getHighLimit()));
        LlValue.setText(String.valueOf(MCT.getLowLimit()));

    }


    /**
     * Permet de sauvgardé dans le bundle avant un changement d'orientation ou le retour du "Background".
     * @param outState Le Bundle qui aura les informations sauvgardées.
     */
    @Override
    protected void onSaveInstanceState(Bundle outState)
    {
        super.onSaveInstanceState(outState);
        if(temperatures != null)
        {
            outState.putParcelableArray("TEMPERATURE", temperatures);
        }

    }

    /**
     * Gère le clique pour ajouter de la valeur a la valeur supérieur.
     * @param view la vue actuel.
     */
    public void AddHighOnClick(View view)
    {
        MCT.onHighLimitUp();
        updateGraph();
    }

    /**
     * Gère le clique pour retirer de la valeur a la valeur supérieur.
     * @param view la vue actuel.
     */
    public void SubHighOnClick(View view)
    {
        MCT.onHighLimitDown();
        updateGraph();
    }

    /**
     * Gère le clique pour ajouter de la valeur a la valeur inférieur.
     * @param view la vue actuel.
     */
    public void AddLowOnClick(View view)
    {
        MCT.onLowLimitUp();
        updateGraph();
    }

    /**
     * Gère le clique pour retirer de la valeur a la valeur inférieur.
     * @param view la vue actuel.
     */
    public void SubLowOnClick(View view)
    {
        MCT.onLowLimitDown();
        updateGraph();
    }

    /**
     * Gère la "fin" de l'activité en retournant à celle précédente.
     * @param view
     */
    public void BackOnClick(View view) {
        if (repoTemperature.insert(temperatures)) {
            Snackbar.make(getWindow().getDecorView().getRootView(), R.string.save, Snackbar.LENGTH_LONG).show();
            //inspirer de https://stackoverflow.com/questions/17237287/how-can-i-wait-for-10-second-without-locking-application-ui-in-android
            Handler handler = new Handler();
            handler.postDelayed(new Runnable() {
                public void run() {
                    finish();
                }
            }, 1000);
        } else {
            Snackbar.make(getWindow().getDecorView().getRootView(), R.string.nosave, Snackbar.LENGTH_LONG).show();
            //inspirer de https://stackoverflow.com/questions/17237287/how-can-i-wait-for-10-second-without-locking-application-ui-in-android
            Handler handler = new Handler();
            handler.postDelayed(new Runnable() {
                public void run() {
                    finish();
                }
            }, 1000);
        }
    }

    /**
     * Permet de mettre à jour le graphique après une modification de la valeur supérieur ou inférieur.
     */
    public void updateGraph()
    {
        graph.removeSeries(seriesMax);
        graph.removeSeries(seriesMin);

        for(int i = 0 ; i<pointsMax.length;i++)
        {
            pointsMax[i] = new DataPoint(i,MCT.getHighLimit());
            pointsMin[i] = new DataPoint(i,MCT.getLowLimit());
        }
        seriesMax = new LineGraphSeries<>(pointsMax);
        seriesMin = new LineGraphSeries<>(pointsMin);

        seriesMax.setColor(getResources().getColor(R.color.blue));
        seriesMin.setColor(getResources().getColor(R.color.red));

        seriesMax.setTitle(getResources().getString(R.string.high_limit));
        seriesMin.setTitle(getResources().getString(R.string.low_limit));

        graph.addSeries(seriesMax);
        graph.addSeries(seriesMin);

        HlValue.setText(String.valueOf(MCT.getHighLimit()));
        LlValue.setText(String.valueOf(MCT.getLowLimit()));


        if(MCT.canRiseLow())
        {
            minusHigh.setEnabled(true);
            plusLow.setEnabled(true);
        }
        else
        {
            minusHigh.setEnabled(false);
            plusLow.setEnabled(false);
        }
    }


}
