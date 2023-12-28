import {
    Box,
    Chip,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    FormControl,
    InputLabel,
    MenuItem,
    OutlinedInput,
    Select,
    SelectChangeEvent,
  } from "@mui/material";
  import { useEffect, useState } from "react";
  import { Button } from "react-bootstrap";
  import axiosInstance from "../AxiosConfig";
  
  interface Participant {
    id: string;
    firstName: string;
    lastName: string;
  }
  
  const AddParticipant = ({ subjectId }: { subjectId: string }) => {
    const [selectedParticipants, setSelectedParticipants] = useState<Participant[]>([]);
    const [show, setShow] = useState(false);
    const [participants, setParticipants] = useState<Participant[]>([]);
  
    useEffect(() => {
      getData();
    }, []);
  
    const getData = () => {
      axiosInstance
        .get(`https://localhost:7136/api/v1.0/users/notRegisteredAtSubject/${subjectId}`)
        .then((result) => {
          setParticipants(result.data.resource);
        })
        .catch((error) => {
          console.log(error);
        });
    };
  
    const handleChange = (event: SelectChangeEvent<typeof selectedParticipants>) => {
      const {
        target: { value },
      } = event;
      setSelectedParticipants(value);
    };
  
    const handleClose = () => {
      setSelectedParticipants([]);
      setShow(false);
    };
  
    const handleShow = () => {
      setShow(true);
    };
  
    const handleSave = () => {
      const userIds = selectedParticipants.map((x) => x.id);
  
      handleClose();
  
      axiosInstance
        .post(`https://localhost:7136/api/v1.0/subjects/${subjectId}/users`, { userIds })
        .then(() => {})
        .catch((error) => console.log(error));
    };
  
    return (
      <div className="add-participant">
        <Button type="button" className="btn btn-success add-user" onClick={handleShow}>
          Add Participant
        </Button>
        <Dialog disableEscapeKeyDown open={show} onClose={handleClose}>
          <DialogTitle>Add Participants</DialogTitle>
          <DialogContent>
            <FormControl>
              <Select
                multiple
                displayEmpty
                value={selectedParticipants}
                onChange={handleChange}
                input={<OutlinedInput />}
                renderValue={(selected) => {
                  if (selected.length === 0) {
                    return <em>Participants</em>;
                  }
  
                  return (
                    <Box sx={{ display: "flex", flexWrap: "wrap", gap: 0.5 }}>
                      {selected.map((participant) => (
                        <Chip label={`${participant.firstName} ${participant.lastName}`} />
                      ))}
                    </Box>
                  );
                }}
              >
                {participants.map((participant) => (
                  <MenuItem key={participant.id} value={participant}>
                    {`${participant.firstName} ${participant.lastName}`}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
          </DialogContent>
          <DialogActions>
            <Button onClick={handleClose} className="btn btn-secondary">
              Cancel
            </Button>
            <Button onClick={handleSave}>Save</Button>
          </DialogActions>
        </Dialog>
      </div>
    );
  };
  
  export default AddParticipant;
  