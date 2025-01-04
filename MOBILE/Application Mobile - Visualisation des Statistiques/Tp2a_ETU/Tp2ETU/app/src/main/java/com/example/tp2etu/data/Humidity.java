package com.example.tp2etu.data;

import android.os.Parcel;
import android.os.Parcelable;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

/**
 * Objet contenant les données du temps et la valeur a ce moment.
 */
public class Humidity implements Parcelable
{
    int id;
    int DBId;
    long timestamp;
    double value;
    double max;
    double min;
    String date;

    /**
     * Constructeur sans parametre permettant a l'application de modifier l'objet.
     */
    public Humidity()
    {
        this.id = 1;
        this.timestamp = 0;
        this.value = 0;
    }

    /**
     * Constructeur permettant de mettre les valeurs de base à l'objet dès la création.
     * @param timestamp La valeur de quand la valeur a été prise.
     * @param value L'humidité enregistrer a un temps précis.
     */
    @JsonCreator
    public Humidity(@JsonProperty("timestamps") long timestamp, @JsonProperty("value") double value) {
        this.id = 1;
        this.timestamp = timestamp;
        this.value = value;
    }

    /**
     * Constructeur Permettant de créer l'objet à partir d'un parcelable.
     * @param in Le parcel avec lequel faire l'objet.
     */
    protected Humidity(Parcel in) {
        id = 1;
        timestamp = in.readLong();
        value = in.readDouble();
    }

    /**
     * Le créateur de parcel.
     */
    public static final Creator<Humidity> CREATOR = new Creator<Humidity>() {
        @Override
        public Humidity createFromParcel(Parcel in) {
            return new Humidity(in);
        }

        @Override
        public Humidity[] newArray(int size) {
            return new Humidity[size];
        }
    };

    /**
     * Permet de savoir avec quel genre d'objet on travaille. (mis au cas ou il serait nécéssaire en partie b.)
     * @return le ID identifiant l'objet.
     */
    public int getId() {
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
     * Permet de savoir la valeur du timestamps de l'object.
     * @return La valeur du timestamp de l'objet.
     */
    @JsonProperty("timestamps")
    public long getTimestamp() {
        return this.timestamp;
    }

    /**
     * Permet d'avoir la valeur a un certain timestamp.
     * @return la valeur a un certain timestamp.
     */
    @JsonProperty("value")
    public double getValue() {
        return this.value;
    }

    /**
     * Permet de modifier la valeur du timestamp d'un objet humidity.
     * @param timestamps La nouvelle valeur du timestamp.
     */
    public void setTimestamps(long timestamps)
    {
        this.timestamp = timestamp;
    }

    /**
     * Permet de modifier la valeur  d'un objet humidity.
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
     * @return La plus grande valeur d'humidité.
     */
    public double getMax() {
        return max;
    }

    /**
     * Permet de définir la plus grande donnée d'un ensemble de la base de donnée.
     * @param max La valeur de la plus grande donnée d'humidité.
     */
    public void setMax(double max) {
        this.max = max;
    }

    /**
     * Permet D'avoir accès a la plus petite données d'un ensemble de la base de donnée.
     * @return La plus petite valeur d'humidité.
     */
    public double getMin() {
        return min;
    }

    /**
     * Permet de définir la plus petite donnée d'un ensemble de la base de donnée.
     * @param min La valeur de la plus petite donnée d'humidité.
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
