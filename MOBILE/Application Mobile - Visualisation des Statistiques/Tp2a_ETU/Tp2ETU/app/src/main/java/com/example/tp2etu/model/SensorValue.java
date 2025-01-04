package com.example.tp2etu.model;


import android.os.Parcel;
import android.os.Parcelable;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Contient la valeur des capteurs avant leur transformation en humidité ou température.
 */
public class SensorValue implements Parcelable
{
	// Valeur sur l'axe de X
    long timeStamp;

    // Valeur sur l'axe de Y
    double value;

    /**
     * Le créateur d'objet SensorValue.
     * @param timestamp La valeur du temps de l'objet.
     * @param value La valeur au moment du timestamp.
     */
    @JsonCreator
    public SensorValue(@JsonProperty("timestamp") long timestamp,
                       @JsonProperty("value") double value)
    {
        this.value= value;
        this.timeStamp = timestamp;
    }

    /**
     * Constructeur de l'objet a partir d'un parcel.
     * @param in La parcel qui faut rajouter les données.
     */
    protected SensorValue(Parcel in) {
        timeStamp = in.readLong();
        value = in.readDouble();
    }

    /**
     * Le créateur de parcel de l'objet.
     */
    public static final Creator<SensorValue> CREATOR = new Creator<SensorValue>() {
        @Override
        public SensorValue createFromParcel(Parcel in) {
            return new SensorValue(in);
        }

        @Override
        public SensorValue[] newArray(int size) {
            return new SensorValue[size];
        }
    };

    /**
     * Permet d'avoir la valeur du timestamp de l'objet.
     * @return Le timestamp de l'objet.
     */
    @JsonProperty("timestamp")
    public long getTimeStamp()
    {
        return timeStamp;
    }

    /**
     * Permet d'avoir la valeur relier à un timestamp.
     * @return La valeur de l'objet.
     */
    @JsonProperty("value")
    public double getValue()
    {
        return value;
    }

    public void setValue(double value)
    {
        this.value = value;
    }

    /**
     * Honnêtement aucune idée.
     * @return Pas mal juste 0.
     */
    @Override
    public int describeContents() {
        return 0;
    }

    /**
     * Permet de mettre les valeurs nécéssaires dans le parcel.
     * @param parcel Le parcel oû il faut mettre les valeurs.
     * @param i Aucune idée.
     */
    @Override
    public void writeToParcel(Parcel parcel, int i)
    {
        parcel.writeLong(timeStamp);
        parcel.writeDouble(value);
    }
}
