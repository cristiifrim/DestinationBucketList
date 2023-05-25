import {
  CircularProgress,
  Container,
  IconButton,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Tooltip,
  Button,
  Box,
  useTheme,
  useMediaQuery,
  Grid,
  Card,
  CardContent,
  Typography,
  CardActions,
} from "@mui/material";

import { useEffect, useState, useContext } from "react";
import { Link, useParams } from "react-router-dom";
import { BACKEND_API_URL } from "../../constants";
import axios, { AxiosError } from "axios";
import { SnackbarContext } from "../SnackbarContext";
import { isAuthorized, getAccount, getAuthToken } from "../../auth";

import AddIcon from "@mui/icons-material/Add";
import ReadMoreIcon from "@mui/icons-material/ReadMore";
import EditIcon from "@mui/icons-material/Edit";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import { Destination } from "../../models/Destination";

export const PrivateDestinations = () => {
  const { userId } = useParams();
  const openSnackbar = useContext(SnackbarContext);
  const [loading, setLoading] = useState(true);
  const [destinations, setDestinations] = useState<Destination[]>([]);

  const theme = useTheme();
  const isSmallScreen = useMediaQuery(theme.breakpoints.down("sm"));
  const isMediumScreen = useMediaQuery(theme.breakpoints.down("md"));
  const isLargeScreen = useMediaQuery(theme.breakpoints.down("lg"));

  const headers = [
    { text: "#", hide: false },
    { text: "Geolocation", hide: false },
    { text: "Title", hide: isLargeScreen },
    { text: "Image", hide: false },
    { text: "Description", hide: false },
    { text: "Operations", hide: false },
  ];

  const fetchDestinations = async () => {
    setLoading(true);
    try {
      await axios
        //api pentru a lua destinatiile pentru un userId dat --> something liek that ig
        .get<Destination[]>(`${BACKEND_API_URL}/destinations/${userId}`, {
          headers: {
            Authorization: `Bearer ${getAuthToken()}`,
          },
        })
        .then((response) => {
          const data = response.data;
          setDestinations(data);
          setLoading(false);
        })
        .catch((reason: AxiosError) => {
          console.log(reason.message);
          openSnackbar(
            "error",
            "Failed to fetch destinations for the given user!\n" +
              (String(reason.response?.data).length > 255
                ? reason.message
                : reason.response?.data)
          );
        });
    } catch (error) {
      console.log(error);
      openSnackbar(
        "error",
        "Failed to fetch destinations due to an unknown error!"
      );
    }
  };

  useEffect(() => {
    fetchDestinations();
  });

  return (
    <Container data-testid="test-all-destinations-container">
      <h1
        style={{
          paddingTop: 26,
          marginBottom: 4,
          textAlign: "center",
        }}
      >
        All Destinations
      </h1>

      {loading && <CircularProgress />}
      {!loading && (
        <Button
          component={Link}
          to={`/destinations`}
          disabled={getAccount() === null}
          variant="text"
          size="large"
          sx={{ mb: 2, textTransform: "none" }}
          startIcon={<AddIcon />}
        >
          Add more destinations to your buscket
        </Button>
      )}
      {!loading && destinations.length === 0 && (
        <p style={{ marginLeft: 16 }}>No destinations found.</p>
      )}
      {!loading &&
        destinations.length > 0 &&
        (isMediumScreen ? (
          <Grid container spacing={3}>
            {destinations.map((destination, index) => (
              <Grid item xs={12} sm={6} md={4} key={destination.id}>
                <Card>
                  <CardContent>
                    <Typography variant="h6" component="div">
                      {destination.title}
                    </Typography>
                  </CardContent>
                  <CardActions>
                    <IconButton
                      component={Link}
                      to={`/destinations/${destination.id}/details`}
                    >
                      <Tooltip title="View destination details" arrow>
                        <ReadMoreIcon color="primary" />
                      </Tooltip>
                    </IconButton>

                    <IconButton
                      component={Link}
                      to={`/destinations/${destination.id}/deletePrivate`}
                      //disabled={!isAuthorized(destination.user?.id)}
                      sx={{ color: "red" }}
                    >
                      <Tooltip title="Remove" arrow>
                        <DeleteForeverIcon />
                      </Tooltip>
                    </IconButton>
                  </CardActions>
                </Card>
              </Grid>
            ))}
          </Grid>
        ) : (
          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 0 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  {headers.map((header, i) => {
                    if (header.hide) {
                      return null;
                    }
                    return (
                      <TableCell
                        key={i}
                        style={{ userSelect: "none" }}
                        align={header.text === "Operations" ? "center" : "left"}
                      >
                        {header.text}
                      </TableCell>
                    );
                  })}
                </TableRow>
              </TableHead>
              <TableBody>
                {destinations.map((destination, index) => {
                  const storeData = [
                    destination.title,
                    <Box
                      display="flex"
                      alignItems="flex-start"
                      justifyContent="center"
                    >
                      <IconButton
                        component={Link}
                        to={`/destinations/${destination.id}/details`}
                      >
                        <Tooltip title="View destination details" arrow>
                          <ReadMoreIcon color="primary" />
                        </Tooltip>
                      </IconButton>

                      <IconButton
                        component={Link}
                        to={`/destinations/${destination.id}/deletePrivate`}
                        //disabled={!isAuthorized(destination.user?.id)}
                        sx={{ color: "red" }}
                      >
                        <Tooltip title="Remove" arrow>
                          <DeleteForeverIcon />
                        </Tooltip>
                      </IconButton>
                    </Box>,
                  ];
                  return (
                    <TableRow key={destination.id}>
                      {storeData.map((data, i) => {
                        const header = headers[i];
                        if (header.hide) {
                          return null;
                        }
                        return (
                          <TableCell
                            key={i}
                            align={
                              header.text === "Operations" ? "center" : "left"
                            }
                          >
                            {data}
                          </TableCell>
                        );
                      })}
                    </TableRow>
                  );
                })}
              </TableBody>
            </Table>
          </TableContainer>
        ))}
    </Container>
  );
};
