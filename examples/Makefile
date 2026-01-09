CC ?= cc
CXX ?= c++
CFLAGS ?=
CXXFLAGS ?=

CFLAGS += -std=c17 -O2 -g -Wall -Wextra -Wpedantic -Wshadow -Wconversion \
          -Wsign-conversion -Wformat=2 -Wcast-align -Wdouble-promotion \
          -fno-omit-frame-pointer
CXXFLAGS += -std=c++20 -O2 -g -Wall -Wextra -Wpedantic -Wshadow -Wconversion \
            -Wsign-conversion -Wformat=2 -Wcast-align -Wdouble-promotion \
            -Woverloaded-virtual -Wold-style-cast -fno-omit-frame-pointer

BUILD_DIR ?= build
APP ?=

APP_DIRS := $(filter-out $(BUILD_DIR)/,$(wildcard */))
APPS := $(APP_DIRS:/=)

all: $(APPS)

$(APPS):
	@$(MAKE) build APP=$@

build:
	@if [ -z "$(APP)" ]; then \
		echo "Usage: make <app> (available: $(APPS))"; \
		exit 1; \
	fi
	@mkdir -p $(BUILD_DIR)
	@src_cpp="$(wildcard $(APP)/*.cpp)"; \
	src_c="$(wildcard $(APP)/*.c)"; \
	if [ -n "$$src_cpp" ]; then \
		$(CXX) $(CXXFLAGS) $$src_cpp -o $(BUILD_DIR)/$(APP); \
	elif [ -n "$$src_c" ]; then \
		$(CC) $(CFLAGS) $$src_c -o $(BUILD_DIR)/$(APP); \
	else \
		echo "No sources found in $(APP)"; \
		exit 1; \
	fi

clean:
	rm -rf $(BUILD_DIR)

.PHONY: all clean build $(APPS)
