import { CssBaseline, Container, Button } from "@mui/material";
import { Link, useLocation } from "react-router-dom";
import vinylsPng from "../assets/vinyls.png";
import React from "react";

export const AppHome = () => {
  const location = useLocation();
  const path = location.pathname;

  return (
    <React.Fragment>
      <CssBaseline />

      <Container
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          padding: "5em",
          height: "100vh",
          maxWidth: "100em!important",
          backgroundColor: "#7286D3 ",
          //"linear-gradient(to bottom,#FFF2F2,#E5E0FF,#8EA7E9, #7286D3)",
        }}
      ></Container>
    </React.Fragment>
  );
};
