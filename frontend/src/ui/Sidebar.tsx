import styled from "styled-components";

export const Sidebar = () => {
  return (
    <StyledSideBar>
      <p>SIDEBAR</p>
    </StyledSideBar>
  );
};

const StyledSideBar = styled.aside`
  background-color: var(--color-grey-0);
  padding: 3.2rem 2.4rem;
  border-bottom: 1px solid var(--color-grey-100);

  grid-row: 1/ -1;
`;
