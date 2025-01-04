package com.example.tp2etu.model;

/**
 * Gère l'objet des zones pour l'activité d'humidité.
 */
public class Zone
{
    int zoneNB;
    int counts;
    double[] datas;
    double higherLimit;
    double lowerLimit;

    /**
     * Le créateur d'objet de zone.
     * @param zoneNB Le nombre de zone voulue.
     * @param datas Le contenant des donnés de la zone.
     */
    public Zone(int zoneNB,double[] datas)
    {
        this.zoneNB = zoneNB;
        this.datas = datas;
        counts = datas.length;
        for(int i = 0;i<datas.length;i++)
        {
            if(i == 0)
            {
                higherLimit = datas[i];
                lowerLimit = datas[i];
            }
            else
            {
                if(datas[i] > higherLimit)
                {
                    higherLimit = datas[i];
                }
                else if(datas[i] < lowerLimit)
                {
                    lowerLimit = datas[i];
                }
            }
        }
    }

    /**
     * Permet d'avoir le nombre de zone demander par l'utilisateur.
     * @return le nombre de zone.
     */
    public int getZoneNB()
    {
        return this.zoneNB;
    }

    /**
     * Permet de modifier le nombre de zone demander par l'utilisateur.
     * @param zoneNB le nouveau nombre de zone demander.
     */
    public void setZoneNB(int zoneNB)
    {
        this.zoneNB = zoneNB;
    }

    /**
     * Permet d'avoir la plus haute valeur de la zone.
     * @return la valeur de la plus haute valeur de la zone.
     */
    public double getHigherLimit()
    {
        return this.higherLimit;
    }

    /**
     * Permet de mettre la nouvelle limite haute de la zone.
     * @param higherLimit La nouvelle plus haute valeur de la zone.
     */
    public void setHigherLimit(double higherLimit)
    {
        this.higherLimit = higherLimit;
    }

    /**
     * Permet d'avoir accès a la limite inférieur de la zone.
     * @return La nouvelle valeur de la limite de la inférieur de la zone.
     */
    public double getLowerLimit()
    {
        return this.lowerLimit;
    }

    /**
     * Permet de modifier la limite inférieur de la zone.
     * @param lowerLimit La nouvelle valeur inférieur de la zone.
     */
    public void setLowerLimit(double lowerLimit)
    {
        this.lowerLimit = lowerLimit;
    }

    /**
     * Le nombre de valeur inclut dans la zone.
     * @return Le nombre de valeur inclut dans la zone.
     */
    public double getCounts()
    {
        return this.counts;
    }

    /**
     * Permet de mettre ou modifier une donnée dans la zone.
     * @param pos la position de la donnée
     * @param data la nouvelle donnée de cette position.
     */
    public void setDatas(int pos,double data)
    {
        this.datas[pos] = data;
    }

    /**
     * Permet d'avoir une donnée a une position particulière.
     * @param pos La position de la donnée a recevoir.
     * @return La valeur de la donnée voulu.
     */
    public double getData(int pos)
    {
        return this.datas[pos];
    }

}

