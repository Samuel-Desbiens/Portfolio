package com.example.tp2etu.model;
//Complétez l'implémentation de la classe avec les méthodes et attributs nécessaires à son fonctionnement

/**
 * Classe utilisé pour gérer les commandes relier a l'activité de temperature. (En majoriter pour les test unitaires)
 */
public class ModelControllerTemperature {

    final double mod = 1;
    double highLimit;
    double lowLimit;

    /**
     * Constructeur du modele et controller de la temperature.
     * @param highLimit La limite supérieur de temperature.
     * @param lowLimit La limite inférieur de temperature.
     */
    public ModelControllerTemperature(double highLimit, double lowLimit) {
        this.highLimit = highLimit;
        this.lowLimit = lowLimit;
    }

    /**
     * Permet de vérifier la possibilité de monter la limite du bas ou de baisser la limite du haut.
     * @return La possibilité de monter la limite du bas ou de baisser la limite du haut.
     */
    public boolean canRiseLow()
    {
        if((this.lowLimit + mod) >= this.highLimit)
        {
            return false ;
        }
        else
        {
            return true;
        }
    }

	//Les méthodes méthodes suivantes sont TOUTES OBLIGATOIRES et leurs signatures DE DOIVENT PAS être modifiées

    /**
     * Permet de modifier la limite supérieur.
     * @param highLimit La nouvelle limite supérieur.
     */
    public void setHighLimit(double highLimit)
    {
        this.highLimit = highLimit;
    }

    /**
     * Permet de savoir la limite supérieur actuel.
     * @return la limite supérieur actuel.
     */
	public double getHighLimit()
    {
		return this.highLimit;
    }

    /**
     * Permet de modifier la limite inférieur.
     * @param lowLimit La nouvelle limite inférieur.
     */
    public void setLowLimit(double lowLimit)
    {
        this.lowLimit = lowLimit;
    }

    /**
     * Permet de savoir la limite inférieur actuel.
     * @return la limite inférieur actuel.
     */
    public double getLowLimit()
    {
		return this.lowLimit;
    }

    /**
     * Gère l'appuie du boutton pour élevé la limite supérieur de la valeur du modifier.
     */
    public void onHighLimitUp() 
	{
	    setHighLimit(this.highLimit+mod);
    }

    /**
     * Gère l'appuie du boutton pour abaissé la limite supérieur de la valeur du modifier en vérifiant
     * la possibilité de le faire.
     */
    public void onHighLimitDown() 
	{
	    if(canRiseLow())
        {
            setHighLimit(this.highLimit-mod);
        }

    }

    /**
     * Gère l'appuie du boutton pour abaissé la limite inférieur de la valeur du modifier.
     */
    public void onLowLimitDown() 
	{
	    setLowLimit(this.lowLimit-mod);
    }

    /**
     * Gère l'appuie du boutton pour monté la limite inférieur de la valeur du modifier en vérifiant
     * la possibilité de le faire.
     */
    public void onLowLimitUp() 
	{
	    if(canRiseLow())
        {
            setLowLimit(this.lowLimit+mod);
        }
    }  
}
