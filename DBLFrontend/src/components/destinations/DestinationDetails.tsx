import {
  Box,
  Card,
  CardActions,
  CardContent,
  IconButton,
  Button,
} from "@mui/material";
import { Container } from "@mui/system";
import { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { BACKEND_API_URL } from "../../constants";
import { Destination } from "../../models/Destination";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import ArrowBackIcon from "@mui/icons-material/ArrowBack";
import AccessTimeFilledIcon from "@mui/icons-material/AccessTimeFilled";

export const DestinationDetails = () => {
  const { destinationId } = useParams();
  const [destination, setDestinations] = useState<Destination>();

  useEffect(() => {
    const fetchDestinations = async () => {
      const response = await fetch(
        `${BACKEND_API_URL}/destinations/${destinationId}`
      );
      const destination = await response.json();
      setDestinations(destination);
    };
    fetchDestinations();
  }, [destinationId]);

  return (
    <Container>
      <Card sx={{ p: 2 }}>
        <CardContent>
          <Box display="flex" alignItems="flex-start">
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
              Destinations Details
            </h1>
          </Box>

          <Box sx={{ ml: 1 }}>
            <p>Title: {destination?.title}</p>
            <p>Description: {destination?.description}</p>
            <p>Geolocation: {destination?.geolocation}</p>
            <p>Start Date: {destination?.startDate.toDateString()}</p>
            <p>End Date: {destination?.endDate.toDateString()}</p>
            <p>Image: {destination?.image}</p>
          </Box>
        </CardContent>
        <CardActions sx={{ mb: 1, ml: 1, mt: 1 }}>
          <Button
            component={Link}
            to={`/destinations/${destinationId}/edit`}
            variant="text"
            size="large"
            sx={{
              color: "gray",
              textTransform: "none",
            }}
            startIcon={<EditIcon />}
          >
            Edit
          </Button>

          <Button
            component={Link}
            to={`/destinations/${destinationId}/delete`}
            variant="text"
            size="large"
            sx={{ color: "red", textTransform: "none" }}
            startIcon={<DeleteForeverIcon />}
          >
            Delete
          </Button>
        </CardActions>
      </Card>
    </Container>
  );
};
