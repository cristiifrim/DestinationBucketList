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

import { useState, useContext, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import DatePicker from "@mui/lab/DatePicker";
import { BACKEND_API_URL, formatDate } from "../../constants";
import axios, { AxiosError } from "axios";
import { SnackbarContext } from "../SnackbarContext";
import { getAuthToken, logOut } from "../../auth";
import { UserRegisterDto } from "../../models/UserRegisterDto";

import ArrowBackIcon from "@mui/icons-material/ArrowBack";

export const UserRegister = () => {
  const navigate = useNavigate();
  const openSnackbar = useContext(SnackbarContext);

  const [user, setUser] = useState<UserRegisterDto>({
    name: "",
    password: "",

    email: "",
    birthday: new Date(),
  });

  useEffect(() => {
    logOut();
  }, []);

  const userRegister = async (event: { preventDefault: () => void }) => {
    event.preventDefault();
    try {
      await axios
        .post(`${BACKEND_API_URL}/users/register`, user, {
          headers: {
            Authorization: `Bearer ${getAuthToken()}`,
          },
        })
        .then((response) => {
          console.log(response);
          const token = response.data.token;

          const expirationDateTime = new Date(response.data.expiration);
          const expirationInMinutes = Math.floor(
            (expirationDateTime.getTime() - new Date().getTime() + 1000 * 59) /
              (1000 * 60)
          );

          openSnackbar(
            "success",
            "Registered successfully!" +
              "\n" +
              "Please confirm your account using this code: " +
              token +
              "\n" +
              `This code will expire in ${expirationInMinutes} minutes at ${formatDate(
                expirationDateTime
              )}.`
          );
          navigate(`/users/register/confirm/${token}`);
        })
        .catch((reason: AxiosError) => {
          console.log(reason.message);
          openSnackbar(
            "error",
            "Failed to register!\n" +
              (String(reason.response?.data).length > 255
                ? reason.message
                : reason.response?.data)
          );
        });
    } catch (error) {
      console.log(error);
      openSnackbar("error", "Failed to register due to an unknown error!");
    }
  };

  return (
    <Container>
      <Card sx={{ p: 2 }}>
        <CardContent>
          <Box display="flex" alignItems="flex-start" sx={{ mb: 4 }}>
            <IconButton component={Link} sx={{ mb: 2, mr: 3 }} to={`/`}>
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
              Register
            </h1>
          </Box>

          <form>
            <TextField
              id="name"
              label="Name"
              variant="outlined"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event) =>
                setUser({
                  ...user,
                  name: event.target.value,
                })
              }
            />
            <TextField
              id="password"
              label="Password"
              variant="outlined"
              type="password"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event) =>
                setUser({
                  ...user,
                  password: event.target.value,
                })
              }
            />

            <DatePicker
              id="endDate"
              label="End Date"
              fullWidth
              sx={{ mb: 2 }}
              onChange={(event: { target: { value: any } }) =>
                setUser({
                  ...user,
                  birthday: event.target.value,
                })
              }
            />
          </form>
        </CardContent>
        <CardActions sx={{ mb: 1, ml: 1, mt: 1 }}>
          <Button onClick={userRegister} variant="contained">
            Register
          </Button>
        </CardActions>
      </Card>
    </Container>
  );
};
