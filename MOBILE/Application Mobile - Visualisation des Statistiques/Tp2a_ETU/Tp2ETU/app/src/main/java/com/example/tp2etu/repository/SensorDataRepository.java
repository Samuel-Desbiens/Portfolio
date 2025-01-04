package com.example.tp2etu.repository;

import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;

import com.example.tp2etu.data.Humidity;
import com.example.tp2etu.data.SensorData;
import com.example.tp2etu.database.SensorDataStatsTable;
import com.example.tp2etu.model.SensorValue;

import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Calendar;

public class SensorDataRepository implements Repository<SensorData>
{

    public boolean insert(SensorData dataObject)
    {
        return false;
    }

    private SQLiteDatabase database;

    public SensorDataRepository(SQLiteDatabase database) {this.database = database;}

    /**
     * Permet de rajouter une donnée dans la base de donnée.
     * @param dataObject L'objet SensorData a rajouter.
     * @return Si l'insertion à marcher ou non.
     */
    @Override
    public boolean insert(SensorData[] dataObject)
    {
        long elapsedTime = Calendar.getInstance().getTime().toInstant().toEpochMilli();
        SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy HH:mm:ss");
        Timestamp ts = new Timestamp(elapsedTime);
        String formattedDate = sdf.format(ts);
        double min = Integer.MAX_VALUE;
        double max = Integer.MIN_VALUE;
        SensorValue[] sensorValues = dataObject[0].getValues();
        for(int i = 0;i<dataObject.length;i++)
        {
            if(min > sensorValues[0].getValue())
            {
                min = sensorValues[0].getValue();
            }
            if(max < sensorValues[0].getValue())
            {
                max = sensorValues[0].getValue();
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
     * @return l'objet SensorData dans la base de donnée a l'endroit de l'id.
     */
    @Override
    public SensorData find(int id)
    {
        SensorData sensorData = null;
        Cursor cursor = null;

        try
        {
            database.beginTransaction();
            cursor = database.rawQuery(SensorDataStatsTable.SELECT_ONE_OF_SQL, new String[]{
                    String.valueOf(id)
            });
            sensorData = new SensorData(null,null);
            sensorData.setDBId(cursor.getInt(0));
            sensorData.setValues(0,(cursor.getDouble(2)));
            sensorData.setValues(1,(cursor.getDouble(3)));
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
            return sensorData;
        }
    }

    /**
     * Permet d'avoir accès à la dernière donnée dans la base de donnée.
     * @return L'objet SensorData qui correspond a la dernière donnée de la base de donnée.
     */
    @Override
    public SensorData findLast() {
        SensorData sensorData = null;
        Cursor cursor = null;

        try
        {
            database.beginTransaction();
            cursor = database.rawQuery(SensorDataStatsTable.SELECT_LAST_OF_SQL, new String[]{});
            sensorData = new SensorData(null,null);
            sensorData.setDBId(cursor.getInt(0));
            sensorData.setValues(0,(cursor.getDouble(2)));
            sensorData.setValues(1,(cursor.getDouble(3)));
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
            return sensorData;
        }
    }

    /**
     * Permet d'avoir accès a toute les donnée dans un tableau d'objet.
     * @return Un tableau d'objet contenant toute les donnée de la base de donnée.
     */
    @Override
    public SensorData[] findAll()
    {
        SensorData[] sensorDatas = null;
        SensorData sd = null;
        Cursor cursor = null;

        try
        {
            database.beginTransaction();
            cursor = database.rawQuery(SensorDataStatsTable.SELECT_ALL_OF_SQL, new String[]{});
            sensorDatas = new SensorData[cursor.getCount()];
            for(int i = 0;i<cursor.getCount();i++)
            {
                sd = new SensorData(null,null);
                sd.setDBId(cursor.getInt(0));
                sd.setDate(cursor.getString(2));
                sd.setMin(cursor.getDouble(3));
                sd.setMax(cursor.getDouble(4));
                sensorDatas[i] = sd;
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
            return sensorDatas;
        }
    }

    /**
     * Permet de modifier une valeur déja présente dans la base de donnée.
     * @return un bool pour savoir si la savgarde de l'information modifié a été bien fait ou non.
     */
    @Override
    public boolean save(SensorData dataObject) {
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
                            String.valueOf(dataObject.getValues()[0].getValue()),
                            String.valueOf(dataObject.getValues()[1].getValue()),
                            String.valueOf(dataObject.getId())

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
    public boolean delete(SensorData dataObject) {
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
