import {
  Box,
  Button,
  Card,
  CircularProgress,
  CardActions,
  CardContent,
  Container,
  IconButton,
  TextField,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
  Autocomplete,
} from "@mui/material";
import DatePicker from "@mui/lab/DatePicker";
import { useCallback, useEffect, useState, useRef } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import axios, { AxiosError } from "axios";
import { Destination } from "../../models/Destination";
import { BACKEND_API_URL } from "../../constants";
import { useContext } from "react";
import { SnackbarContext } from "../SnackbarContext";

export const DestinationUpdate = () => {
  const navigate = useNavigate();
  const openSnackbar = useContext(SnackbarContext);

  const { destinationId } = useParams<{ destinationId: string }>();

  const [loading, setLoading] = useState(false);
  const [destination, setDestination] = useState<Destination>({
    geolocation: "",
    description: "",

    title: "",
    image: "",

    startDate: new Date(),
    endDate: new Date(),

    isPublic: true
  });

  useEffect(() => {
    const fetchDestination = async () => {
      const response = await fetch(
        `${BACKEND_API_URL}/destinations/${destinationId}/`
      );

      const destination = await response.json();
      setDestination({
        id: destination.id,
        geolocation: destination.geolocation,
        description: destination.description,

        title: destination.title,
        image: destination.image,

        startDate: destination.startDate,
        endDate: destination.endDate,

        isPublic: true
      });

      setLoading(false);
    };
    fetchDestination();
  }, [destinationId]);

  const handleUpdate = async (event: { preventDefault: () => void }) => {
    event.preventDefault();
    try {
      await axios
        .put(`${BACKEND_API_URL}/destinations/${destinationId}/`, destination)
        .then(() => {
          openSnackbar("success", "Destination updated successfully!");
          navigate("/destinations");
        })
        .catch((reason: AxiosError) => {
          console.log(reason.message);
          openSnackbar(
            "error",
            "Failed to update destination!\n" + reason.response?.data
          );
        });
    } catch (error) {
      console.log(error);
      openSnackbar(
        "error",
        "Failed to update destination due to an unknown error!"
      );
    }
  };

  const handleCancel = (event: { preventDefault: () => void }) => {
    event.preventDefault();
    navigate("/destinations");
  };

  const convertToInputFormat = (dateString?: string) => {
    if (!dateString) return "";
    const date = new Date(dateString);
    const localDateString = date.toISOString().slice(0, 16);
    return localDateString;
  };

  return (
    <Container>
      {loading && <CircularProgress />}
      {!loading && (
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
                Edit Destination
              </h1>
            </Box>

            <form onSubmit={handleUpdate}>
              <TextField
                id="geolocation"
                label="Geolocation"
                variant="outlined"
                fullWidth
                sx={{ mb: 2 }}
                onChange={(event) =>
                  setDestination({
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
                  setDestination({
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
                  setDestination({
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
                  setDestination({
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
                  setDestination({
                    ...destination,
                    endDate: event.target.value,
                  })
                }
              />
            </form>
          </CardContent>
          <CardActions sx={{ mb: 1, ml: 1, mt: 1 }}>
            <Button
              type="submit"
              onClick={handleUpdate}
              variant="contained"
              sx={{ width: 100, mr: 2 }}
            >
              Save
            </Button>
            <Button
              onClick={handleCancel}
              variant="contained"
              color="error"
              sx={{ width: 100 }}
            >
              Cancel
            </Button>
          </CardActions>
        </Card>
      )}
    </Container>
  );
};
