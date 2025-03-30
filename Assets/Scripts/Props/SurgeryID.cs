using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SurgeryID : MonoBehaviour
{
    public TMP_Dropdown procedureDropdown, equipmentDropdown, medicationDropdown, durationDropdown;
    public TextMeshProUGUI responseText, surgeryIDText;
    public List<Surgery> surgeries;
    public List<int> surgeryID;

    Surgery inputedSurgery;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateID()
    {
        inputedSurgery = new Surgery((Procedure)procedureDropdown.value, (Equipment)equipmentDropdown.value, (Medication)medicationDropdown.value, (Duration)durationDropdown.value);
        Response response = GetResponse();

        int randomNumber = UnityEngine.Random.Range(0, 999999);
        string ID = randomNumber.ToString("000000");

        switch (response)
        {
            case Response.Error:
                surgeryID = new List<int>();
                responseText.text = "There was an error validating this surgery. Please make sure your fields are correct.";
                surgeryIDText.text = "Error";
                break;

            case Response.GrantAccess:
                surgeryID = new List<int>(ID.ToIntArray());
                responseText.text = "Surgery logged. Due to the type logged, the generated ID will grant access to the equipment room.";
                surgeryIDText.text = ID;
                break;

            case Response.NoAccess:
                responseText.text = "Surgery logged. Please check with management or security in order to gain access to the equipment room.";
                surgeryID = new List<int>(ID.ToIntArray());
                surgeryIDText.text = ID;
                break;
        }
    }

    public bool SameSurgery(Surgery value, Surgery compareTo)
    {
        if (value.procedure == compareTo.procedure && value.equipment == compareTo.equipment && value.medication == compareTo.medication && value.duration == compareTo.duration) return true;
        else return false;
    }

    public Response GetResponse()
    {
        foreach (Surgery surgery in surgeries)
        {
            if (SameSurgery(inputedSurgery, surgery))
            {
                return surgery.response;
            }
        }
        return Response.Error;
    }
}

[Serializable]
public class Surgery
{
    public Procedure procedure;
    public Equipment equipment;
    public Medication medication;
    public Duration duration;
    public Response response;

    public Surgery(Procedure procedure, Equipment equipment, Medication medication, Duration duration)
    {
        this.procedure = procedure;
        this.equipment = equipment;
        this.medication = medication;
        this.duration = duration;
    }
}

public enum Procedure
{
    GeneralSurgery,
    Cardiothoriac,
    Neurosurgery,
    Urology,
    Orthopaedic
}

public enum Equipment
{
    None,
    CPBPump,
    Curettes,
    Cystoscope,
    BoneSaw
}

public enum Medication
{
    Anesthesia,
    Anticoagulants,
    Paralytics,
    Benzodiazepines,
    Antibiotics
}

public enum Duration
{
    LessThanAnHour,
    OneToTwoHours,
    ThreeToFourHours,
    FiveToSevenHours,
    MoreThanSevenHours
}

public enum Response
{
    Error,
    GrantAccess,
    NoAccess
}

