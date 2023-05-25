import {
  Container,
  Card,
  CardContent,
  IconButton,
  CardActions,
  Button,
  Box,
} from "@mui/material";
import { Link, useNavigate, useParams } from "react-router-dom";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import axios, { AxiosError } from "axios";
import { BACKEND_API_URL } from "../../constants";
import { useContext } from "react";
import { SnackbarContext } from "../SnackbarContext";

export const DestinationDelete = () => {
  const navigate = useNavigate();
  const openSnackbar = useContext(SnackbarContext);
  const { destinationId } = useParams();

  const handleDelete = async (event: { preventDefault: () => void }) => {
    event.preventDefault();
    try {
      await axios
        .delete(`${BACKEND_API_URL}/destinations/${destinationId}`)
        .then(() => {
          openSnackbar("success", "Destination deleted successfully!");
          navigate("/destinations");
        })
        .catch((reason: AxiosError) => {
          console.log(reason.message);
          openSnackbar(
            "error",
            "Failed to delete destination!\n" + reason.response?.data
          );
        });
    } catch (error) {
      console.log(error);
      openSnackbar(
        "error",
        "Failed to delete destination due to an unknown error!"
      );
    }
  };

  const handleCancel = (event: { preventDefault: () => void }) => {
    event.preventDefault();
    navigate("/destinations");
  };

  return (
    <Container>
      <Card sx={{ p: 2 }}>
        <CardContent>
          <Box display="flex" alignItems="flex-start">
            <IconButton component={Link} sx={{ mr: 3 }} to={`/destinations`}>
              <ArrowBackIcon />
            </IconButton>
            <h1
              style={{
                flex: 1,
                textAlign: "center",
                marginTop: -4,
                marginLeft: -64,
              }}
            >
              Delete Destination
            </h1>
          </Box>

          <p style={{ marginBottom: 0, textAlign: "center" }}>
            Are you sure you want to delete this destination? This cannot be
            undone!
          </p>
        </CardContent>
        <CardActions
          sx={{
            mb: 1,
            mt: 1,
            display: "flex",
            justifyContent: "center",
          }}
        >
          <Button
            variant="contained"
            color="error"
            sx={{ width: 100, mr: 2 }}
            onClick={handleDelete}
          >
            Yes
          </Button>
          <Button
            variant="contained"
            color="primary"
            sx={{ width: 100 }}
            onClick={handleCancel}
          >
            Cancel
          </Button>
        </CardActions>
      </Card>
    </Container>
  );
};
