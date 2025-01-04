package com.example.tp2etu;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;

import androidx.test.ext.junit.runners.AndroidJUnit4;
import androidx.test.platform.app.InstrumentationRegistry;


import com.example.tp2etu.database.MyDatabaseFactory;
import com.example.tp2etu.data.SensorData;
import com.example.tp2etu.repository.SensorDataRepository;

// Vous devrez ajuster les imports ici selon votre projet.

import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.FixMethodOrder;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.junit.runners.MethodSorters;

@FixMethodOrder(MethodSorters.NAME_ASCENDING)
@RunWith(AndroidJUnit4.class)
public class RepositoryTest {

    SQLiteDatabase database = null;
    private SensorDataRepository repo;

    @After
    public void tearDown() {
        database.close();
    }

    @Before
    public void setUp() throws Exception {
        Context appContext = InstrumentationRegistry.getInstrumentation().getTargetContext();
        MyDatabaseFactory helper = new MyDatabaseFactory(appContext, null);
        database = helper.getWritableDatabase();
        repo = new SensorDataRepository(database);
    }

    
    @Test
    public void T_Create01_canCreateEmptyDatabase()
    {
        // Arrange

        //Act
        SensorData foundData =  repo.findLast();

        //Assert
        Assert.assertEquals(null, foundData);
    }   
}
