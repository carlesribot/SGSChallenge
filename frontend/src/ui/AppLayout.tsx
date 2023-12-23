import { Outlet } from "react-router-dom";
import { Header } from "./Header";

import styled from "styled-components";

export const AppLayout = () => {
  return (
    <StyleAppLayout>
      <Header />
      <Main>
        <Container>
          <Outlet />
        </Container>
      </Main>
    </StyleAppLayout>
  );
};

const Main = styled.main`
  background-color: var(--color-grey-50);
  padding: 4rem 4.8rem 6.4rem;
  overflow: scroll;
`;

const StyleAppLayout = styled.div`
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: auto 1fr;
  height: 100vh;
`;

const Container = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  grid-gap: 16px; /* Adjust the gap between grid items */
  margin: 0 auto; /* Optional: Center the container horizontally */
  max-width: 120rem;
`;
