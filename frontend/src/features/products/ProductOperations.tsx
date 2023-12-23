import { Filter } from "../../ui/Filter";
import SortBy from "../../ui/SortBy";
import TableOperations from "../../ui/TableOperations";

export const ProductTableOperations = () => {
  return (
    <TableOperations>
      <Filter
        filterField="filteredBy"
        options={[
          { value: "all", label: "All" },
          { value: "active", label: "Active" },
          { value: "inactive", label: "Inactive" },
          { value: "available", label: "With Stock" },
        ]}
      />

      <SortBy
        options={[
          { value: "name-asc", label: "Sort by name (A-Z)" },
          { value: "name-desc", label: "Sort by name (Z-A)" },
          { value: "price-asc", label: "Sort by price (low first)" },
          { value: "price-desc", label: "Sort by price (high first)" },
          { value: "category-asc", label: "Sort by category (A-Z)" },
          { value: "category-desc", label: "Sort by category (Z-A)" },
        ]}
      />
    </TableOperations>
  );
};
