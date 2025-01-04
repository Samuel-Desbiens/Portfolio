package com.example.tp2etu.repository;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.example.tp2etu.data.Humidity;
import com.example.tp2etu.data.SensorData;
import com.example.tp2etu.database.SensorDataStatsTable;

import java.sql.Time;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Calendar;

/**
 * Permet de gérer la connection entre l'interface et la base de donnée pour les données d'humidité.
 */
public class HumidityRepository implements Repository<Humidity>
{

    private SQLiteDatabase database;

    public HumidityRepository(SQLiteDatabase database) {this.database = database;}

    /**
     * Permet de rajouter une donnée dans la base de donnée.
     * @param dataObject L'objet humidité a rajouter.
     * @return Si l'insertion à marcher ou non.
     */
    @Override
    public boolean insert(Humidity[] dataObject)
    {
        long elapsedTime = Calendar.getInstance().getTime().toInstant().toEpochMilli();
        SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
        Timestamp ts = new Timestamp(elapsedTime);
        String formattedDate = sdf.format(ts);
        double min = Integer.MAX_VALUE;
        double max = Integer.MIN_VALUE;
        for(int i = 0;i<dataObject.length;i++)
        {
            if(min > dataObject[i].getValue())
            {
                min = dataObject[i].getValue();
            }
            if(max < dataObject[i].getValue())
            {
                max = dataObject[i].getValue();
            }

        }

        try
        {
            database.beginTransaction();
            database.execSQL(SensorDataStatsTable.INSERT_SQL, new String[]
                    {
                            String.valueOf(dataObject[0].getId()),
                            formattedDate,
                            String.valueOf(min),
                            String.valueOf(max)
                    });
            database.setTransactionSuccessful();
        }
        catch (RuntimeException e)
        {
            return false;
        }
        finally
        {
            database.endTransaction();
            return true;
        }
    }

    /**
     * Permet de trouver une donné précise dans la base de donnée avec un id.
     * @param id l'id de la donnée a trouver.
     * @return l'objet humidité dans la base de donnée a l'endroit de l'id.
     */
    @Override
    public Humidity find(int id)
    {
       Humidity humidity = null;
       Cursor cursor = null;

        try
        {
            database.beginTransaction();
            cursor = database.rawQuery(SensorDataStatsTable.SELECT_ONE_OF_SQL, new String[]{
                    String.valueOf(id)
            });
            humidity = new Humidity();
            humidity.setDBId(cursor.getInt(0));
            humidity.setDate(cursor.getString(2));
            humidity.setMin(cursor.getDouble(3));
            humidity.setMax(cursor.getDouble(4));
            database.setTransactionSuccessful();
        }
        catch (RuntimeException e)
        {
            return null;
        }
        finally
        {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
            return humidity;
        }
    }

    /**
     * Permet d'avoir accès à la dernière donnée dans la base de donnée.
     * @return L'objet humidity qui correspond a la dernière donnée de la base de donnée.
     */
    @Override
    public Humidity findLast() {
        Humidity humidity = null;
        Cursor cursor = null;

        try
        {
            database.beginTransaction();
            cursor = database.rawQuery(SensorDataStatsTable.SELECT_LAST_OF_SQL, new String[]{});
            if(cursor.moveToNext())
            {
                if(cursor.getLong(0)> 0)
                {
                    humidity = new Humidity();
                    humidity.setDBId(cursor.getInt(0));
                    humidity.setDate(cursor.getString(2));
                    humidity.setMin(cursor.getDouble(3));
                    humidity.setMax(cursor.getDouble(4));
                }
            }
            database.setTransactionSuccessful();
        }
        catch (RuntimeException e)
        {
            return null;
        }
        finally
        {
            if (cursor != null)
            {
                cursor.close();
            }
            database.endTransaction();
            return humidity;
        }
    }

    /**
     * Permet d'avoir accès a toute les donnée dans un tableau d'objet.
     * @return Un tableau d'objet contenant toute les donnée de la base de donnée.
     */
    @Override
    public Humidity[] findAll()
    {
        Humidity[] humidities = null;
        Humidity humi = null;
        Cursor cursor = null;

        try
        {
            database.beginTransaction();
            cursor = database.rawQuery(SensorDataStatsTable.SELECT_ALL_OF_SQL, new String[]{});
            humidities = new Humidity[cursor.getCount()];
            for(int i = 0;i<cursor.getCount();i++)
            {
                humi = new Humidity();
                humi.setDBId(cursor.getInt(0));
                humi.setDate(cursor.getString(2));
                humi.setMin(cursor.getDouble(3));
                humi.setMax(cursor.getDouble(4));
                humidities[i] = humi;
                cursor.moveToNext();
            }
            database.setTransactionSuccessful();
        }
        catch (RuntimeException e)
        {
            return null;
        }
        finally
        {
            if (cursor != null) {
                cursor.close();
            }
            database.endTransaction();
            return humidities;
        }
    }

    /**
     * Permet de modifier une valeur déja présente dans la base de donnée.
     * @return un bool pour savoir si la savgarde de l'information modifié a été bien fait ou non.
     */
    @Override
    public boolean save(Humidity dataObject) {
        try
        {
            long elapsedTime = Calendar.getInstance().getTime().toInstant().toEpochMilli();
            SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
            Timestamp ts = new Timestamp(elapsedTime);
            String formattedDate = sdf.format(ts);

            database.beginTransaction();

            database.execSQL(SensorDataStatsTable.UPDATE_SQL, new String[]
                    {
                            formattedDate,
                            String.valueOf(1),
                            String.valueOf(dataObject.getMin()),
                            String.valueOf(dataObject.getMax()),
                            String.valueOf(dataObject.getDBId())

                    });
            database.setTransactionSuccessful();

        }
        catch (RuntimeException e)
        {
            return false;
        }
        finally
        {
            database.endTransaction();
            return true;
        }
    }

    /**
     * Permet de retirer un objet de la base de donnée qui correspond a l'objet passer en paramêtre.
     * @param dataObject L'objet a retirer de la base de donnée.
     * @return Si la supprésion à marcher ou pas.
     */
    @Override
    public boolean delete(Humidity dataObject) {
        try {
            database.beginTransaction();
            database.rawQuery(SensorDataStatsTable.DELETE_SQL, new String[]{String.valueOf(dataObject.getDBId())});

            database.setTransactionSuccessful();
        } catch (RuntimeException e) {
            return false;
        } finally {
            database.endTransaction();
            return true;
        }
    }

    /**
     * Permet la supprésion d'un object de la base de donnée avec la position de celui-ci dans la base de donnée
     * @param id L'id de l'objet a supprimé de la base de donnée.
     * @return Si La supprésion fut réussi ou échouer.
     */
    @Override
    public boolean delete(int id) {
        try {
            database.beginTransaction();
            database.rawQuery(SensorDataStatsTable.DELETE_SQL, new String[]{String.valueOf(id)});

            database.setTransactionSuccessful();
        } catch (RuntimeException e) {
            return false;
        } finally {
            database.endTransaction();
            return true;
        }
    }
}
