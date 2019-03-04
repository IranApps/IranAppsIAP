package com.iranapps;

import android.app.Activity;
import android.content.SharedPreferences;
import android.util.Log;
import android.widget.Toast;

import java.lang.reflect.Field;
import java.lang.reflect.Method;
import java.lang.reflect.InvocationTargetException;

public class IranAppsIABPluginBase
{
	private static IranAppsIABPlugin mInstance;
	protected static final String TAG = "[IranAppsAIB][Plugin]";
	protected static final String MANAGER_NAME = "IranAppsPlugin.IABEventManager";
	private Class<?> mUnityPlayerClass;
	private Field mUnityPlayerActivityField;
	private Method mUnitySendMessageMethod;
	
	public static IranAppsIABPlugin instance()
	{
		if (mInstance == null)
			mInstance = new IranAppsIABPlugin();
		
		return mInstance;
	}
	
	public IranAppsIABPluginBase()
	{
		try
		{
			// Using reflection to remove reference to Unity library.
			mUnityPlayerClass = Class.forName("com.unity3d.player.UnityPlayer");
			mUnityPlayerActivityField = mUnityPlayerClass.getField("currentActivity");
			mUnitySendMessageMethod = mUnityPlayerClass.getMethod("UnitySendMessage", new Class[] { String.class, String.class, String.class });
		}
		catch (ClassNotFoundException e)
		{
			Log.i(TAG, "Could not find UnityPlayer class: " + e.getMessage());
		}
		catch (NoSuchFieldException e)
		{
			Log.i(TAG, "Could not find currentActivity field: " + e.getMessage());
		}
		catch (Exception e)
		{
			Log.i(TAG, "Unkown exception occurred locating UnitySendMessage(): " + e.getMessage());
		}
	}
	
	protected Activity getActivity()
	{
		if (mUnityPlayerActivityField != null) 
		{
			try
			{
				Activity activity = (Activity)mUnityPlayerActivityField.get(mUnityPlayerClass);
				if (activity == null) 
					Log.e(TAG, "The Unity Activity does not exist. This could be due to a low memory situation");
				
				return activity;
			}
			catch (Exception e)
			{
				Log.i(TAG, "Error getting currentActivity: " + e.getMessage());
			}
		}
		return null;
	}
	
	protected void UnitySendMessage(String methodName, String methodParam)
	{
		if (methodParam == null)
			methodParam = "";
		
		if (mUnitySendMessageMethod != null)
		{
			try
			{
				mUnitySendMessageMethod.invoke(null, new Object[]{ MANAGER_NAME, methodName, methodParam });
			}
			catch (IllegalArgumentException e)
			{
				Log.i(TAG, "could not find UnitySendMessage method: " + e.getMessage());
			}
			catch (IllegalAccessException e)
			{
				Log.i(TAG, "could not find UnitySendMessage method: " + e.getMessage());
			}
			catch (InvocationTargetException e)
			{
				Log.i(TAG, "could not find UnitySendMessage method: " + e.getMessage());
			}
		}
		else
		{
			Toast.makeText(getActivity(), "UnitySendMessage:\n" + methodName + "\n" + methodParam, Toast.LENGTH_LONG).show();
			Log.i(TAG, "UnitySendMessage: IranAppsIABManager, " + methodName + ", " + methodParam);
		}
	}
	
	protected void runSafelyOnUiThread(Runnable r)
	{
		runSafelyOnUiThread(r, null);
	}
	
	protected void runSafelyOnUiThread(final Runnable r, final String methodName)
	{
		getActivity().runOnUiThread(new Runnable()
		{
			public void run()
			{
				try
				{
					r.run();
				}
				catch (Exception e)
				{
					if (methodName != null) 
					{
						UnitySendMessage(methodName, e.getMessage());
					}
					Log.e(TAG, "Exception running command on UI thread: " + e.getMessage());
				}
			}
		});
	}
	
	protected void persist(String key, String value)
	{
		IABLogger.logEntering(getClass().getSimpleName(), "persist", new Object[] { key, value });
		try
		{
			SharedPreferences prefs = getActivity().getSharedPreferences("IranAppsIABPluginPreferences", 0);
			prefs.edit().putString(key, value).commit();
		}
		catch (Exception e)
		{
			Log.i(TAG, "error in persist: " + e.getMessage());
		}
	}
	
	protected String unpersist(String key, boolean deleteKeyAfterFetching)
	{
		IABLogger.logEntering(getClass().getSimpleName(), "unpersist", new Object[] { key, Boolean.valueOf(true) });
		
		String val = "";
		try
		{
			SharedPreferences prefs = getActivity().getSharedPreferences("IranAppsIABPluginPreferences", 0);
			val = prefs.getString(key, null);
			if (deleteKeyAfterFetching) {
				prefs.edit().remove(key).commit();
			}
			return val;
		}
		catch (Exception e)
		{
			Log.i(TAG, "error in unpersist: " + e.getMessage());
		}
		return val;
	}
}
