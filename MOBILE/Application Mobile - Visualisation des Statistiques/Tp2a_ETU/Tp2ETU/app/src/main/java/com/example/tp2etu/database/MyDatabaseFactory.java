package com.example.tp2etu.database;


import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Permet d'avoir accès a la base de donnée et gère la création de celle-ci.
 */
public class MyDatabaseFactory extends SQLiteOpenHelper {

    private static final String DATABASE_NAME = "TP2bPartie2";
    private static final int DATABASE_VERSION = 1;

    /**
     * Le constructeur de la DatabasFactory gérant la connection au base de donnée.
     * @param context Aucune idée.
     * @param databaseName Le nom de la base de donnée.
     */
    public MyDatabaseFactory(Context context, String databaseName) {
        super(context, databaseName, null, DATABASE_VERSION);
    }

    /**
     * Le constructeur de la DatabasFactory gérant la connection au base de donnée.
     * @param context Aucune idée.
     */
    public MyDatabaseFactory(Context context) {
        this(context, DATABASE_NAME);
    }

    /**
     * Le créateur de la base de donnée si celle-ci n'éxiste pas.
     * @param db La base de donnée
     */
    @Override
    public void onCreate(SQLiteDatabase db) {

        db.execSQL(SensorDataStatsTable.CREATE_TABLE_SQL);
    }

    /**
     * Permet de prendre une nouvelle version de la base de donnée en écrasant l'ancienne.
     * @param db La base de donné en mémoire.
     * @param oldVersion Ancienne version de la base de donnée.
     * @param newVersion La nouvelle version de la base de donnée.
     */
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL(SensorDataStatsTable.DROP_TABLE_SQL);
        onCreate(db);
    }
}
