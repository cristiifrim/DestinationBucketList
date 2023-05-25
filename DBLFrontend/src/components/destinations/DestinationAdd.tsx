import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  IconButton,
  TextField,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
} from "@mui/material";
import { Container } from "@mui/system";
import DatePicker from "@mui/lab/DatePicker";
import { useState, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { BACKEND_API_URL } from "../../constants";
import axios, { AxiosError } from "axios";
import { SnackbarContext } from "../SnackbarContext";
import { getAuthToken } from "../../auth";

import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import { Destination } from "../../models/Destination";

export const DestinationAdd = () => {
  const navigate = useNavigate();
  const openSnackbar = useContext(SnackbarContext);

  const [destination, setDestinations] = useState<Destination>({
    geolocation: "",
    title: "",
    image: "",
    description: "",
    startDate: new Date(),
    endDate: new Date(),

    isPublic: true
  });

  const addDestination = async (event: { preventDefault: () => void }) => {
    event.preventDefault();
    try {
      await axios
        .post(`${BACKEND_API_URL}/destinations`, destination, {
          headers: {
            Authorization: `Bearer ${getAuthToken()}`,
          },
        })
        .then(() => {
          openSnackbar("success", "Destination added successfully!");
          navigate("/destinations");
        })
        .catch((reason: AxiosError) => {
          console.log(reason.message);
          openSnackbar(
            "error",
            "Failed to add destination!\n" +
              (String(reason.response?.data).length > 255
                ? reason.message
                : reason.response?.data)
          );
        });
    } catch (error) {
      console.log(error);
      openSnackbar(
        "error",
        "Failed to add destination due to an unknown error!"
      );
    }
  };

  return (
    <Container>
      <Card sx={{ p: 2 }}>
        <CardContent>
          <Box display="flex" alignItems="flex-start" sx={{ mb: 4 }}>
            <IconButton
              component={Link}
              sx={{ mb: 2, mr: 3 }}
              to={`/destinations`}
            >
              <ArrowBackIcon />
            </IconButton>
            <h1
              style={{
                flex: 1,
                textAlign: "center",
                marginLeft: -64,
                marginTop: -4,
              }}
            >
              Add Destination
            </h1>
          </Box>

          <form>
            <TextField
              id="geolocation"
              label="Geolocation"
              variant="outlined"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event) =>
                setDestinations({
                  ...destination,
                  geolocation: event.target.value,
                })
              }
            />
            <TextField
              id="title"
              label="Title"
              variant="outlined"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event) =>
                setDestinations({
                  ...destination,
                  title: event.target.value,
                })
              }
            />
            <Button id="image" variant="contained" component="label">
              Upload File
              <input type="file" hidden />
            </Button>

            <TextField
              id="description"
              label="Description"
              variant="outlined"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event) =>
                setDestinations({
                  ...destination,
                  description: event.target.value,
                })
              }
            />

            <DatePicker
              id="startDate"
              label="Start Date"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event: { target: { value: any } }) =>
                setDestinations({
                  ...destination,
                  startDate: event.target.value,
                })
              }
            />

            <DatePicker
              id="endDate"
              label="End Date"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event: { target: { value: any } }) =>
                setDestinations({
                  ...destination,
                  endDate: event.target.value,
                })
              }
            />

            {/* <TextField
              id="openDate"
              label="Open Date"
              InputLabelProps={{ shrink: true }}
              type="datetime-local"
              variant="outlined"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event) =>
                setStore({
                  ...store,
                  openDate: new Date(event.target.value).toISOString(),
                })
              }
            />

            <TextField
              id="closeDate"
              label="Close Date"
              InputLabelProps={{ shrink: true }}
              type="datetime-local"
              variant="outlined"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event) =>
                setStore({
                  ...store,
                  closeDate: new Date(event.target.value).toISOString(),
                })
              }
            /> */}
          </form>
        </CardContent>
        <CardActions sx={{ mb: 1, ml: 1, mt: 1 }}>
          <Button onClick={addDestination} variant="contained">
            Add Destination
          </Button>
        </CardActions>
      </Card>
    </Container>
  );
};
