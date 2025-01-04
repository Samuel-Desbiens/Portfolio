package com.example.tp2etu.model;

import com.example.tp2etu.data.Humidity;
import com.example.tp2etu.data.SensorData;

/**
 * Classe utilisé pour gérer les commandes relier a l'activité d'humidité. (En majoriter pour les test unitaires)
 */
public class ModelControllerHumidity {
	public static final double EPSILON = 0.001;
	public int nbZones;
	public int selectedZone;
	public Zone[] zones;
	public SensorValue[] sv;
	public Humidity[] humidities;

    /**
     * Constructeur du modele et controller de l'activité humidité.
     * @param humidities Le tableau des valeurs d'humidité.
     */
	public ModelControllerHumidity(Humidity[] humidities)
    {
        this.nbZones = humidities.length;
        selectedZone = 0;
        this.humidities = humidities;
        double[] dataTransfer = new double[1];
        zones = new Zone[this.nbZones];
        for(int i = 0;i<this.nbZones;i++)
        {
            dataTransfer[0] = humidities[i].getValue();
            zones[i] = new Zone(i,dataTransfer);
        }
    }

    /**
     * Permet d'avoir la donnée a l'intérieur d'une certaine valeur a l'intérieur d'une zone.
     * @param zone Le numéro de la zone ou chercher la donnée.
     * @param pos La position de la donnée a l'intérieur d'une zone
     * @return la valeur de la donné rechercher.
     */
    public double getZoneData(int zone,int pos)
    {
        return zones[zone].getData(pos);
    }

    /**
     * Fonction mettant à jour les zones après un changement du nombre de zone;
     * @param datasPerZone Le nombre de donné supposé par zone
     */
    public void updateZones(int datasPerZone)
    {
        zones = new Zone[this.nbZones];
        selectedZone = 0;
        double[] dataTransfer = new double[datasPerZone];
        for(int i = 0;i<this.nbZones;i++)
        {
            for(int o = 0;o<dataTransfer.length;o++)
            {
                try
                {
                    dataTransfer[o] = humidities[(i*dataTransfer.length)+o].getValue();
                }
                catch (ArrayIndexOutOfBoundsException e)
                {
                    break;
                }
            }
            zones[i] = new Zone(i,dataTransfer);
        }
    }
	//Les méthodes méthodes suivantes sont TOUTES OBLIGATOIRES et leurs signatures DE DOIVENT PAS être modifiées

    /**
     * Constructeur du modele et controller de l'activité humidité.(Pour test unitaire)
     * @param p le SensorData dont les données vont provenir.
     */
    public ModelControllerHumidity(SensorData p)
    {
        this.nbZones = p.getValues().length;
        selectedZone = 0;
        sv = p.getValues();
        double[] dataTransfer = new double[1];
        zones = new Zone[p.getValues().length];
        for(int i = 0;i<p.getValues().length;i++)
        {
            dataTransfer[0] = sv[i].value;
            zones[i] = new Zone(i,dataTransfer);
        }
    }

    /**
     * Permet de modifier le nombre de zone.
     * @param nbZones La nouveau nombre de zone.
     */
    public void setNbZones(int nbZones)
    {
		this.nbZones = nbZones;
    }

    /**
     * Permet de savoir le nombre de zone.
     * @return le nombre de zone actuel.
     */
    public int getNbZones()
    {
		return nbZones;
    }

    /**
     * Permet d'avoir le nombre de valeur dans chaque zone.
     * @param zone La zone à compter
     * @return Le nombre de valeur dans la zone spécifier.
     */
    public int getCountInZone(int zone)
    {
		return zones[zone].counts;
    }

    /**
     * Permet d'avoir la plus haute valeur d'une zone.
     * @param zone la zone qu'on veut la plus haute valeur.
     * @return la plus haute valeur de la zone spécifier.
     */
    public double getUpperLimit(int zone)
    {
		return zones[zone].getHigherLimit();
    }

    /**
     * Permet d'avoir la plus basse valeur d'une zone.
     * @param zone la zone qu'on veut la plus basse valeur.
     * @return la plus basse valeur de la zone spécifier.
     */
    public double getLowerLimit(int zone)
    {
		return zones[zone].getLowerLimit();
    }

    /**
     * Permet de savoir la zone a afficher les limites sélectionner par l'utilisateur.
     * @return la zone sélectionner par l'utilisateur.
     */
    public int getSelectedZone()
    {
		return selectedZone;
    }

    /**
     * Permet de modifier la zone sélectionner par l'uttilisateur.
     * @param selectedZone La nouvelle zone sélectionner par l'utilisateur.
     */
    public void setSelectedZone(int selectedZone)
    {
       this.selectedZone = selectedZone;
    }
}
