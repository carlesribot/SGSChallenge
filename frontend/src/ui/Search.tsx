import styled from "styled-components";
import { useSearchParams } from "react-router-dom";
import { ChangeEvent, useState } from "react";
import { FaSearch } from "react-icons/fa";

export const Search = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const search = searchParams.get("search") || "";
  const [query, setQuery] = useState(search);

  const handleOnKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === "Enter") {
      searchParams.set("search", query);
      setSearchParams(searchParams);
    }
  };

  const handleInputChange = (event: ChangeEvent<HTMLInputElement>) => {
    setQuery(event.target.value);
  };

  return (
    <SearchBox>
      <Input
        placeholder="Search products"
        value={query}
        onKeyDown={handleOnKeyDown}
        onChange={handleInputChange}
      ></Input>
      <ImgButton>
        <FaSearch />
      </ImgButton>
    </SearchBox>
  );
};

const SearchBox = styled.div`
  border: 1px solid #66666654;
  display: flex;
  padding: 0px 8px;
  padding-left: 10px;
  min-height: 32px;
  align-items: center;
  border-radius: 4px;
  background-color: #00000014;
  margin-bottom: 2rem;
`;

const ImgButton = styled.button`
  color: gray.800;
  margin: 0;
  border: none;
  outline: none !important;
  appearance: none;
  border-radius: 4px;
  background-color: transparent;
  -webkit-tap-highlight-color: transparent;
`;

const Input = styled.input`
  flex: 1;
  color: gray.800;
  margin: 0;
  border: none;
  outline: none !important;
  font-size: 14px;
  appearance: none;
  user-select: text;
  border-radius: 4px;
  background-color: transparent;
  -webkit-tap-highlight-color: transparent;

  &:focus:focus-visible {
    box-shadow: none !important;
  }

  &:hover {
    background-image: none !important;
  }
`;
