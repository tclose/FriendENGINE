//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{

	public class FriendEngineComm
		{
		     struct configurationPair
		     {
				   private readonly String studyTag;
				   private readonly String tagValue;
				
				   public configurationPair (String studyTagParam, String valueParam)
				   {
					   this.studyTag = studyTagParam;
					   this.tagValue = valueParam;
				   }
				
				   public String StudyTag { get { return studyTag; } }
				   public String Value { get { return tagValue; } }
			
		     }

		     protected s_TCP mainThread, responseThread;
			 protected String HostData;
		     protected String checkingValue;
			 protected Int32 Port;
			 protected Int32 actualState = 0;
			 protected Int32 actualPhase = 0;
		     protected Int32 actualStepState = 0;
		     protected Int32 actualCommState = 0;
		     protected Int32 operation = 0;
		     protected string sessionID;
		     protected int actualVolume;
		     protected String actualCommand;
			 protected String actualVariable;
			 protected String actualValue;
		     protected int runSize;
		     List<configurationPair> pairs;

		     public String lastSessionComandResponse;
		     public String lastGraphResponse;
		     public String lastFeedBack;
		     public String lastFeedBackClass;
		     public double[] feedbackClasses;
		     public double[] feedbackValues; 
		     public int FeedbackFailed;
		     public int feedbackRun = 1;

		     public Int32 doTRAIN = 0;
		     public Int32 doGLM = 0;
		     public Int32 doFEATURESELECTION = 0;

		   
			 public FriendEngineComm ()
			 {
				   mainThread = new s_TCP();
				   responseThread = new s_TCP();

				   actualState = 0;
			       actualCommState = 0; 
			       actualStepState = 0;
			       actualPhase = 0;

			       pairs = new List<configurationPair>();
			 }
		     
		     public void addConfigurationPair(String studyTag, String valueParam)
		     {
			     configurationPair pair = new configurationPair(studyTag, valueParam);
			     pairs.Add(pair);
		     }


		     public void setupConnection(String host, Int32 port)	
		     {
			       HostData = host;
				   Port = port;
			 }

		     public void configureEngine()
		     {
			      foreach(configurationPair pair in pairs)
			      {
  					 setVarCommand(pair.StudyTag, pair.Value);
				     do {}
				     while (stateManager() != 0);
				  }
			 }

		     public void setRunsize(int size)	{
				   feedbackValues = new double[size];
				   feedbackClasses = new double[size];
			       runSize = size;
			 }

		     public void connect()	{
			      mainThread.setupSocket (HostData, Port);
  			 }

		     // create a new session in Friend Engine
		     public void createSession() {
			     operation = 1;
			     actualState = 1;
			     handleCreateSession();
		     }

		     // Sets the plugin information  
		     public void setupPlugIn() {
			     operation = 2;
			     actualState = 1;
			     handlePlugInSetup();
		     }

		     public void issueCommand(String command)
			 {
 			     actualCommand = command;
			     operation = 3;
			     actualState = 1;
			     handleIssueCommand();
		     }

		     public void getSessionCommandState(String value)
			 {
			     checkingValue = value;
			     operation = 4;
			     actualState = 1;
			     handleGetSessionCommandState();
		     }

		     public void getGraphParams(int volume)
			 {
			     actualVolume = volume;
			     operation = 5;
			     actualState = 1;
			     handleGetGraphParams();
		     }

		     public void getFeedBack(int volume)
			 {
			     actualVolume = volume;
			     operation = 6;
			     actualState = 1;
			     if (volume > runSize)
			     {
				    actualState = 0;
				    FeedbackFailed = 0;
			     }
			     handleGetFeedBack();
		     }

		     public void endSession()
		     {
			     operation = 7;
			     actualState = 1;
			     handleEndSession();
		     }  

		     public void setVarCommand(String variable, String value)
		     {
			     operation = 8;
			     actualState = 1;
				 actualVariable = variable;
				 actualValue = value;

			     handleSetVarCommand();
			 }

 		     public int stateManager()
		     {
			    if (!mainThread.Connected ()) 
			    {
				   operation = 0;
				   return operation;
			    }

			    if (operation == 1)
				   handleCreateSession();

			    if (operation == 2)
			   	   handlePlugInSetup();

			    if (operation == 3)
			   	   handleIssueCommand();

			    if (operation == 4)
			   	   handleGetSessionCommandState();

			    if (operation == 5)
			   	   handleGetGraphParams();

			    if (operation == 6)
			   	   handleGetFeedBack();

			    if (operation == 7)
			   	   handleEndSession();

			    if (operation == 8)
			   	   handleSetVarCommand();

			    return operation;
			 }

		     protected virtual void handleCreateSession() 
		     {
			     String response; 
			     if (actualState == 1) {
				    mainThread.writeSocket ("NEWSESSION");
				    actualState = 2;
				 }

			     if (actualState == 2) {
				    sessionID = mainThread.readSocket();
				    if (sessionID != "") actualState = 3;
				 }

			     if (actualState == 3) {
				    response = mainThread.readSocket();
				    if (response != "") actualState = 0;
				 }

			     if (actualState == 0) operation = 0;
			 }

		     protected virtual void handleSetVarCommand() 
		     {
			     String response; 
			     if (actualState == 1) {
				    mainThread.writeSocket ("SET");
				    mainThread.writeSocket (actualVariable);
				    mainThread.writeSocket (actualValue);
				    actualState = 2;
				 }

			     if (actualState == 2) {
				    response = mainThread.readSocket();
				    if (response != "") actualState = 0;
				 }

			     if (actualState == 3) {
				    response = mainThread.readSocket();
				 }

			     if (actualState == 0) operation = 0;
			 }

		     protected virtual void writePluginInfo()
		     {
			    mainThread.writeSocket ("PLUGIN");
			    mainThread.writeSocket ("libROI");
			    mainThread.writeSocket ("no");
			    mainThread.writeSocket ("processROI");
			    mainThread.writeSocket ("initializeROIProcessing");
			    mainThread.writeSocket ("finalizeProcessing");
			    mainThread.writeSocket ("no");
			    mainThread.writeSocket ("no");
			 }

		     protected virtual void handlePlugInSetup() 
		     {
			     String response; 
			     if (actualState == 1) {
				    writePluginInfo();
				    actualState = 2;
				 }

			     if (actualState == 2) {
				    response = mainThread.readSocket();
				    if (response != "") actualState = 0;
				 }

			     if (actualState == 0) operation = 0;
			 }

		     protected virtual void handleIssueCommand() 
		     {
			     String response; 
			     if (actualState == 1) {
				    mainThread.writeSocket(actualCommand);
				    actualState = 2;
				 }

			     if (actualState == 2) {
				    response = mainThread.readSocket();
				    if (response != "") actualState = 0;
				 }

			     if (actualState == 0) operation = 0;
			 }

		     protected virtual void handleGetSessionCommandState() 
		     {
			     String response;

			     if (actualState == 1) {
					responseThread.setupSocket(HostData, Port);
	    			responseThread.writeSocket("SESSION");
	    			responseThread.writeSocket(sessionID);
				    actualState = 2;
				 }

			     if (actualState == 2) 
			     {
					 response = responseThread.readSocket();
					 if (response == "OK")
					 {
						responseThread.writeSocket(checkingValue);
					    actualState = 3;
					 }
				     else if (response != "") 
					 {
					    actualState = 0;
					    responseThread.closeSocket();
					    lastSessionComandResponse = response;
				     }
				 }

			     // getting the status of the session command
			     if (actualState == 3)
				 {
					response = responseThread.readSocket();
				    if (response != "") responseThread.closeSocket();
					if (response == "OK") actualState = 0;
				    else actualState = 1;
				    lastSessionComandResponse = response;
				 }

			     if (actualState == 0) operation = 0;
			 }

		     protected virtual void handleGetGraphParams() 
		     {
			     String response; 

			     if (actualState == 1) {
					responseThread.setupSocket(HostData, Port);
					responseThread.writeSocket("SESSION");
					responseThread.writeSocket(sessionID);
				    actualState = 2;
				 }

			     if (actualState == 2) {
					response = responseThread.readSocket();
					if (response == "OK")
					{
						responseThread.writeSocket("GRAPHPARS");
						responseThread.writeSocket(actualVolume.ToString());
					    actualState = 3;
					}
				    else if (response != "") 
					{
					   responseThread.closeSocket();
					   actualState = 0;
					   lastGraphResponse = response;
					}
				    else if ((response == "") && (!responseThread.Connected()))
					{
					   responseThread.closeSocket();
					   actualState = 0;
					   lastGraphResponse = "GRAPHPARS";
					}
				 }

			     if (actualState == 3) {
					response = responseThread.readSocket();
					if (response != "")
					{
					    lastGraphResponse = response;
					    actualState = 4;
					}
				    else if ((response == "") && (!responseThread.Connected()))
					{
					   responseThread.closeSocket();
					   actualState = 0;
					   lastGraphResponse = "GRAPHPARS";
					}
				 }

			     if (actualState == 4) {
					response = responseThread.readSocket();
				    if ((response != "") || (!responseThread.Connected()))
					{
					   responseThread.closeSocket();
					   actualState = 0;
					}
				 }

			     if (actualState == 0) operation = 0;
			 }

		     protected virtual void handleGetFeedBack() 
		     {
			     String response; 
			     if (!mainThread.Connected ()) 
		   	     {
				    operation = 0;
				    feedbackValues[actualVolume-1] = 0;
				    feedbackClasses[actualVolume-1] = 0;
				    return;
			     }

			     if (actualState == 1) {
					responseThread.setupSocket(HostData, Port);
					responseThread.writeSocket("SESSION");
					responseThread.writeSocket(sessionID);
				    FeedbackFailed = 0;
				    actualState = 2;
				 }

			     if (actualState == 2) {
					response = responseThread.readSocket();
					if (response == "OK") 
					{
						responseThread.writeSocket ("TEST");
						responseThread.writeSocket (actualVolume.ToString());
					    actualState = 3;
					} 
				    else if (response != "") 
					{
					   responseThread.closeSocket();
					   actualState = 0;
					   FeedbackFailed = 1;
					   lastGraphResponse = response;
					}
				 }

			     if (actualState == 3) {
					lastFeedBackClass = responseThread.readSocket();
					if (lastFeedBackClass != "") 
					{
					   actualState = 4;
					   feedbackClasses[actualVolume-1] = double.Parse(lastFeedBackClass);
					}
				    else if ((lastFeedBackClass == "") && (!responseThread.Connected()))
					 {
					    responseThread.closeSocket();
					    FeedbackFailed = 1;
					    actualState = 0;
					 }
				 }

			     if (actualState == 4) {
					lastFeedBack = responseThread.readSocket();
					if (lastFeedBack != "") 
					{
					   actualState = 5;
					   feedbackValues[actualVolume-1] = double.Parse(lastFeedBack);
					}
				    else if ((lastFeedBack == "") && (!responseThread.Connected()))
					 {
					    responseThread.closeSocket();
					    actualState = 0;
					    FeedbackFailed = 1;
					 }
				 }

			     if (actualState == 5) {
					 response = responseThread.readSocket();
					 //if ((response != "") || (!responseThread.Connected()))
					 {
					    responseThread.closeSocket();
					    actualState = 0;
					 }
				 }

			     if (actualState == 0) operation = 0;
			 }

		     protected virtual void handleEndSession()
		     {
			     String response;
			     int terminate = 0;

			     if (!mainThread.Connected())
				 {
					mainThread.closeSocket();
					actualState = 0;
			     }
			     if (actualState == 1) {
	    			mainThread.writeSocket("ENDSESSION");
	    			mainThread.writeSocket(sessionID);
				    actualState = 2;
				 }

			     if (actualState == 2) 
			     {
					 response = responseThread.readSocket();
					 if (response == "OK") terminate = 1;
				     else if ((response == "") && (!mainThread.Connected())) terminate=1;
				     if (terminate == 1)
				     {
					     mainThread.closeSocket();
					     actualState = 0;
				     }
				 }

			     if (actualState == 0) operation = 0;
		     }

		     public void handleGraphInformation()
		     {
			    if ((actualCommState > 11) && (actualCommState < 100))
			    {
				    if (actualStepState == 0)
				    {
					   getGraphParams(actualVolume);
					   actualStepState = 1;
				    }
				
				    if (actualStepState == 1)
				    {
					   if (stateManager() == 0)
					 	   actualStepState = 2;
				    }
				
				    if (actualStepState == 2)
				    {
					   getFeedBack(actualVolume);
					   actualStepState = 3;
				    }
				
				    if (actualStepState == 3)
				    {
					   if (stateManager() == 0)
						   actualStepState = 4;
			 	    }
				
				    if (actualStepState == 4)
				    {
					   if ((lastGraphResponse != "GRAPHPARS") && (lastGraphResponse != "END"))
					   {
						   actualVolume++;
					   }
					   actualStepState = 0;  
				    }

				    if (lastGraphResponse == "END") 
				    {
					   actualCommState = 100;
					   posprocessing();
					   endSession();
				    }
			   };
			 }
		                            
		     public void posprocessing()
			 {
 			     if (doGLM != 0)
 			     {
				    issueCommand("GLM");
				    do {}
				    while (stateManager() != 0);
			     }

 			     if (doFEATURESELECTION != 0)
 			     {
				    issueCommand("FEATURESELECTION");
				    do {}
				    while (stateManager() != 0);
			     }

 			     if (doTRAIN != 0)
 			     {
				    issueCommand("TRAIN");
				    do {}
				    while (stateManager() != 0);
			     }
		     }

		     public Int32 state()
			 {
			    return actualCommState;
			 } 

		     public void setState(Int32 value)
			 {
			    actualCommState = value;
			 } 

		     public int volume()
			 {
			    return actualVolume;
			 } 

		     public Int32 phase()
			 {
			    return actualPhase;
			 } 

		     public void setPhase(Int32 value)
			 {
			    actualPhase = value;
			 } 

		     public void coreCommunication()
		     {
			    // connecting and creating a new session
			    if (actualCommState == 0) 
			    {
				   connect();
				   createSession();
				   actualCommState = 1;
			    }
			
			    // waiting the session creation
			    if (actualCommState == 1)
			    {
				   if (stateManager() == 0)
					  actualCommState = 2;
			    }
			
			    // setting the plug-in information
			    if (actualCommState == 2)
			    {
				   configureEngine();
				   setupPlugIn();
				   actualCommState = 3;
			    }
			
			    // waiting the plugin configuration
			    if (actualCommState == 3)
			    {
				   if (stateManager() == 0)
				  	   actualCommState = 4;
			    } 
			
			    // Issuing the Preproc Command  
			    if (actualCommState == 4)
			    {
				   issueCommand("NBPREPROC");
				   actualCommState = 5;
			    };
			
			    // waiting the command acknowledge
			    if (actualCommState == 5) 
			    {
				   if (stateManager() == 0)
				 	   actualCommState = 6;
			    };
			
			    // initiate preprocessing query
			    if (actualCommState == 6) 
			    {
				   // sending FEEDBACK command
				   getSessionCommandState("PREPROC");
				   actualCommState = 7;
			    }
			
			    // waiting the end of preprocessing 
			    if (actualCommState == 7) 
			    {
				   if (stateManager() == 0)
					   actualCommState = 8;
			    }
			
			    // initiate feedback processing
			    if (actualCommState == 8) 
			    {
				   // sending FEEDBACK command
				   if (feedbackRun == 1) issueCommand("NBFEEDBACK");
				   else issueCommand("NBPIPELINE");
				   actualCommState = 9;
			    }
			
			    // waiting acknowledge
			    if (actualCommState == 9)
			    {
				   if (stateManager() == 0)
					   actualCommState = 10;
			    }
			
			    // getting the first graph parameter
			    if (actualCommState == 10)
			    {
				   actualVolume = 1;
				   getGraphParams(actualVolume);
				   actualCommState = 11;
			    }
			
			    // waiting the end of graph call
			    if (actualCommState == 11)
			    {
				   if (stateManager() == 0)
					  actualCommState = 12;
			    }
			
			    // starting the first path
			    if (actualCommState == 12)
			    {
				   if (lastGraphResponse == "GRAPHPARS") actualCommState = 10;
				   else
				   {
					   actualCommState = 15;
					   actualPhase = 0;
				   }
			    }
		}
	}
}

