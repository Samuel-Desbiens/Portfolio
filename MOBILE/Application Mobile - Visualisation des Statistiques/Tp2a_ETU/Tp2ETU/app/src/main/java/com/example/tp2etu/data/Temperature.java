package com.example.tp2etu.data;

import android.os.Parcel;
import android.os.Parcelable;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class Temperature implements Parcelable {
    int id;
    int DBId;
    String date;
    long timestamp;
    double value;
    double max;
    double min;

    /**
     * Constructeur de l'objet avec les valeur de base pour la modification de celles-ci.
     */
    public Temperature()

    {
        this.id = 0;
        this.timestamp = 0;
        this.value = 0;
    }

    /**
     * Constructeur avec les variables de l'objet.
     * @param timestamp Le timestamp de l'objet.
     * @param value La valeur lier a un timestamp de l'objet.
     */
    @JsonCreator
    public Temperature(@JsonProperty("timestamp") long timestamp, @JsonProperty("value") double value)
    {
        this.id = 0;
        this.timestamp = timestamp;
        this.value = value;
    }

    /**
     * Constructeur permettant de créer un object a partir d'un parcel.
     * @param in Le parcel avec lequel créer l'objet.
     */
    protected Temperature(Parcel in) {
        id = 0;
        timestamp = in.readLong();
        value = in.readDouble();
    }

    /**
     * Le créateur de parcel pour l'objet temperature.
     */
    public static final Creator<Temperature> CREATOR = new Creator<Temperature>() {
        @Override
        public Temperature createFromParcel(Parcel in) {
            return new Temperature(in);
        }

        @Override
        public Temperature[] newArray(int size) {
            return new Temperature[size];
        }
    };
    /**
     * Permet de savoir avec quel genre d'objet on travaille. (mis au cas ou il serait nécéssaire en partie b.)
     * @return le ID identifiant l'objet.
     */
    public int getId()
    {
        return this.id;
    }

    /**
     * Permet d'avoir l'id de l'enregistrement dans la base de donnée.
     * @return l'id de l'enregistrement dans la base de donnée.
     */
    public int getDBId(){return this.DBId;}

    /**
     * Permet de mettre en mémoire l'id de l'enregistrement dans la base de donnée.
     * @param DBId L'id de l'enregistrement dans la base de donnée.
     */
    public void setDBId(int DBId)
    {
        this.DBId = DBId;
    }
    /**
     * Permet de savoir la valeur du timestamps de l'objet.
     * @return La valeur du timestamp de l'objet.
     */
    @JsonProperty("timestamp")
    public long getTimestamp()
    {
        return this.timestamp;
    }
    /**
     * Permet d'avoir la valeur a un certain timestamp.
     * @return la valeur a un certain timestamp.
     */
    @JsonProperty("value")
    public double getValue()
    {
        return this.value;
    }

    /**
     * Permet de modifier la valeur du timestamp d'un objet temperature.
     * @param timestamp La nouvelle valeur du timestamp.
     */
    public void setTimestamps(long timestamp)
    {
        this.timestamp = timestamp;
    }

    /**
     * Permet de modifier la valeur d'un objet temperature.
     * @param value La nouvelle valeur de l'objet
     */
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
     * Permet de créer un objet parcelable avec l'objet actuel.
     * @param parcel Permet d'avoir l'objet parcelable auquel on rajoute l'information transformer en byte.
     * @param i Aucune idée.
     */
    @Override
    public void writeToParcel(Parcel parcel, int i)
    {
        parcel.writeLong(timestamp);
        parcel.writeDouble(value);
    }

    // Série de donnée nécéssaire à la BD

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
