package com.example.tp2etu.data;


import com.example.tp2etu.model.SensorID;
import com.example.tp2etu.model.SensorValue;
import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Permet d'avoir accès au valeur sérialiser.
 */
public class SensorData
{
    SensorID id = SensorID.TEMPERATURE_ID;

    int DBId;
    double max;
    double min;
    String date;

    SensorValue[] values;

    /**
     * Constructeur de SensorData avec l'id et les valeurs désérialiser des fichiers.
     * @param id L'identification du type de donnée du sensor.
     * @param values Les valeurs venant des fichiers.
     */
    @JsonCreator
    public SensorData(@JsonProperty("id") SensorID id,
                      @JsonProperty("datas") SensorValue[] values)
    {
        this.values= values;
        this.id= id;
    }

    /**
     * Permet d'avoir accès au valeurs SensorValue contenue dans l'objet.
     * @return Le tableau d'objet SensorValue.
     */
    @JsonProperty("datas")
    public SensorValue[] getValues()
    {
        return values;
    }

    public void setValues(int pos,double value)
    {
        if(values == null)
        {
            values = new SensorValue[2];
        }
        values[pos].setValue(value);
    }



    /**
     * Le type de donnée venant des Sensors.
     * @return L'id relier au type de donner présent sur l'objet.
     */
    @JsonProperty("id")
    public SensorID getId()
    {
        return id;
    }

    public void setId(int id)
    {
        if(id == 1)
        {
            this.id = SensorID.HUMIDITY_ID;
        }
        else
        {
            this.id = SensorID.TEMPERATURE_ID;
        }
    }
    //Donnée nécessaire pour l'interaction avec la base de donnée.

    /**
     * Permet d'avoir l'id de l'enregistrement dans la base de donnée.
     * @return l'id de l'enregistrement dans la base de donnée.
     */
    public int getDBId()
    {
        return this.DBId;
    }

    /**
     * Permet de mettre en mémoire l'id de l'enregistrement dans la base de donnée.
     * @param DBId L'id de l'enregistrement dans la base de donnée.
     */
    public void setDBId(int DBId)
    {
        this.DBId = DBId;
    }

    /**
     * Permet D'avoir accès a la plus grande données d'un ensemble de la base de donnée.
     * @return La plus grande valeur de température.
     */
    public double getMax() {
        return max;
    }

    /**
     * Permet de définir la plus grande donnée d'un ensemble de la base de donnée.
     * @param max La valeur de la plus grande donnée de température.
     */
    public void setMax(double max) {
        this.max = max;
    }

    /**
     * Permet D'avoir accès a la plus petite données d'un ensemble de la base de donnée.
     * @return La plus petite valeur de température.
     */
    public double getMin() {
        return min;
    }

    /**
     * Permet de définir la plus petite donnée d'un ensemble de la base de donnée.
     * @param min La valeur de la plus petite donnée de humidité.
     */
    public void setMin(double min) {
        this.min = min;
    }

    /**
     * Permet d'avoir la date d'enregistrement de l'ensemble de donnée.
     * @return La date de l'enregistrement en format string.
     */
    public String getDate()
    {
        return date;
    }

    /**
     * Permet de mettre en mémoire la date reçu de la base de donnée.
     * @param date La date de l'enregistrement dans la base de donné.
     */
    public void setDate(String date)
    {
        this.date = date;
    }

}
